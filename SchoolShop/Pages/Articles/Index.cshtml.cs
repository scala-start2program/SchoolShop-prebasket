using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Helpers;
using SchoolShop.Models;

namespace SchoolShop.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        private readonly int ItemsPerPage = 3;

        public Availability Availability { get; set; }
        public Pagination Pagination { get; private set; }
        public IList<Article> Article { get; set; }
        public List<SelectListItem> AvailableBrands { get; set; }
        public List<SelectListItem> AvailableCategories { get; set; }
        public int? SelectedBrandId { get; set; }
        public int? SelectedCategoryId { get; set; }
        public IndexModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }
        public void OnGet(int? pageIndex, int? brandFilter, int? categoryFilter)
        {
            Availability = new Availability(_context, HttpContext);
            SelectedCategoryId = categoryFilter;
            SelectedBrandId = brandFilter;
            PopulateCollections(null, categoryFilter, pageIndex);

        }
        private void PopulateCollections(int? brandFilter, int? categoryFilter, int? pageIndex)
        {
            // artikels uit database halen volgens ingestelde filters
            IQueryable<Article> artquery = _context.Article
                .Include(b => b.Brand)
                .Include(c => c.Category)
                .OrderBy(a => a.Category.CategoryName)
                .ThenBy(a => a.Price);
            if (brandFilter != null && categoryFilter == null)
            {
                artquery = artquery.Where(b => b.BrandId.Equals(brandFilter));
            }
            if (brandFilter == null && categoryFilter != null)
            {
                artquery = artquery.Where(b => b.CategoryId.Equals(categoryFilter));
            }
            if (brandFilter != null && categoryFilter != null)
            {
                artquery = artquery.Where(b => b.BrandId.Equals(brandFilter) && b.CategoryId.Equals(categoryFilter));
            }

            // paginatie regelen
            Pagination = new Pagination(artquery, pageIndex, ItemsPerPage);
            if (pageIndex > Pagination.LastPageIndex)
            {
                Pagination.PageIndex = 0;
                Pagination.FirstObjectIndex = 0;
            }


            // IQueryable filteren op artikels van de geselecteerde
            // pagina en omzetten in een list

            Article = artquery.Skip(Pagination.FirstObjectIndex)
                        .Take(ItemsPerPage)
                        .ToList();

            // merken ophalen om combobox te vullen
            AvailableBrands = _context.Brand.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.BrandName
                }).OrderBy(b => b.Text).ToList();
            AvailableBrands.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle merken ---"
            });

            // categorien ophalen om combobox te vullen
            AvailableCategories = _context.Category.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.CategoryName
                }).OrderBy(b => b.Text).ToList();
            AvailableCategories.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle categoriën ---"
            });

        }

        public void OnPost(int? brandFilter, int? categoryFilter, int? pageIndex)
        {
            Availability = new Availability(_context, HttpContext);
            SelectedBrandId = brandFilter;
            SelectedCategoryId = categoryFilter;
            PopulateCollections(brandFilter, categoryFilter, pageIndex);
        }
    }


}
