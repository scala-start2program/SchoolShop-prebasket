using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Helpers;
using SchoolShop.Models;

namespace SchoolShop.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        // PRIVATE FIELDS
        private readonly SchoolShop.Data.SchoolShopContext _context;

        // PROPERTIES
        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public Score Score { get; set; }

        public Availability Availability { get; set; }

        public List<Score> Reviews { get; set; }
        public int ScoreCount { get; set; } = 0;
        public int? PreviousId { get; set; }
        public int? NextId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        private List<Article> Articles { get; set; }

        //CONSTRUCTOR
        public DetailsModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        // ONGET & ONPOST
        public IActionResult OnGet(int? id, int? categoryid, int? brandid)
        {
            Availability = new Availability(_context, HttpContext);
            if (id == null)
            {
                return NotFound();
            }
            Article = GetArticle(id);
            if (Article == null)
            {
                return NotFound();
            }
            Score = GetPersonalScore();
            CalculateNumberOfScores();
            Articles = GetArticles(id, categoryid, brandid);
            BrandId = brandid;
            CategoryId = categoryid;
            HandlePagingButtons(id);
            Reviews = BuildReviews();

            return Page();
        }

        public void OnPost(int? star, int? savecomment)
        {
            Availability = new Availability(_context, HttpContext);
            Article = GetArticle(Article.Id);
            if (star != null)
            {
                HandleStars((int)star);
                UpdateAverageScore();
            }
            string comment = "";
            if (savecomment != null)
            {
                comment = Score.Comment;
            }
            CalculateNumberOfScores();
            Score = GetPersonalScore();
            if (savecomment != null)
            {
                if (savecomment == 1)
                    UpdateComment(comment);
                else
                    RemoveComment();
            }
            Reviews = BuildReviews();
        }

        // METHODS

        private Article GetArticle(int? id)
        {
            // vul de PROP Article a.d.h.v. het meegeleverde id
            return _context.Article
                .Include(a => a.Brand)
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);
        }
        private List<Article> GetArticles(int? id, int? categoryid, int? brandid)
        {
            // vraag alle artikels op
            // sorteer ze op dezelfde manier als de index pagina
            // filter ze op dezelfde manier als de index pagina
            IQueryable<Article> artquery = _context.Article
                            .OrderBy(a => a.Category.CategoryName)
                            .ThenBy(a => a.Price);
            if (brandid != null && categoryid == null)
            {
                artquery = artquery.Where(b => b.BrandId.Equals(brandid));
            }
            if (brandid == null && categoryid != null)
            {
                artquery = artquery.Where(b => b.CategoryId.Equals(categoryid));
            }
            if (brandid != null && categoryid != null)
            {
                artquery = artquery.Where(b => b.BrandId.Equals(brandid) && b.CategoryId.Equals(categoryid));
            }
            return artquery.ToList();
        }
        private void HandlePagingButtons(int? id)
        {
            PreviousId = null;
            NextId = null;
            // ga op zoek naar het id van vorige en volgende
            for (int i = 0; i < Articles.Count; i++)
            {
                if (((Article)Articles[i]).Id == id)
                {
                    if (i > 0)
                        PreviousId = ((Article)Articles[i - 1]).Id;
                    if (i < Articles.Count - 1)
                        NextId = ((Article)Articles[i + 1]).Id;
                    break;
                }
            }
            if (PreviousId == null) PreviousId = id;
            if (NextId == null) NextId = id;
        }
        private Score GetPersonalScore()
        {
            // vul de prop Score met het record uit de tabel Score
            // die door de betrokken gebruiker eventueel eerder al
            // al gemaakt heeft (anders gewoon Null)
            if (!string.IsNullOrEmpty(Availability.UserId))
            {
                int userId = int.Parse(Availability.UserId);
                Score = _context.Scores
                    .FirstOrDefault(s => s.UserId == userId && s.ArticleId == Article.Id);
                return Score;
            }
            else
                return null;
        }
        private List<Score> BuildReviews()
        {
            // we halen alle records op uit de tabel score 
            // (met join naar tabel users)
            // voor het actieve artikel
            // we vullen hiermee de PROP Reviews  (List<Score>)
            IQueryable<Score> reviewQuery = _context.Scores
                .Include(s => s.User)
                .Where(s => s.ArticleId.Equals(Article.Id))
                .Where(s => s.Comment.Trim().Length > 0)
                ;
            return reviewQuery.ToList();

        }
        private void HandleStars(int stars)
        {
            // de bezoeker heeft op één van de knoppen met sterretjes geklit
            // we kijken na of betrokken bezoeker ooit eerder al een oordeel geveld heeft
            int userId = int.Parse(Availability.UserId);
            Score = _context.Scores
                .FirstOrDefault(s => s.UserId == userId && s.ArticleId == Article.Id);
            // is dat niet zo, dan maken we een nieuw record aan in de tabel Score
            if (Score == null)
            {
                Score = new Score();
                Score.UserId = userId;
                Score.ArticleId = Article.Id;
                Score.Stars = stars;
                Score.Comment = "";
                _context.Scores.Add(Score);
                _context.SaveChanges();
            }
            // is dat wel zo, dan zoeken we dat record op en passen we dat aan
            else
            {
                Score.Stars = stars;
                _context.Attach(Score).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }
        private void UpdateAverageScore()
        {
            // bereken de som van alle scores voor dit ene artikel 
            int total = _context.Scores.Where(s => s.ArticleId == Article.Id).Sum(s => s.Stars);
            // bereken het aantal scores voor dit ene artikel
            int count = _context.Scores.Where(s => s.ArticleId == Article.Id).Count();
            // bereken het gemiddelde hiervan
            decimal score = 1.0M * total / count;
            // haal het artikel object op (het record van het betrokken artikel)
            Article = _context.Article.FirstOrDefault(m => m.Id == Article.Id);
            // pas de score aan van dit artikel volgens het gemiddelde dat we zonet berekend hebben
            Article.Score = score;
            // bewaar het record
            _context.Attach(Article).State = EntityState.Modified;
            _context.SaveChanges();
        }
        private void CalculateNumberOfScores()
        {
            // bereken het aantal scores voor dit ene artikel
            ScoreCount = _context.Scores.Where(s => s.ArticleId == Article.Id).Count();
        }

        private void UpdateComment(string comment)
        {
            Score.Comment = comment;
            _context.Attach(Score).State = EntityState.Modified;
            _context.SaveChanges();
        }
        private void RemoveComment()
        {
            Score.Comment = "";
            _context.Attach(Score).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
