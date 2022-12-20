using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCenter.Models;

namespace ShoppingCenter.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ShoppingCenterContext _context;

        public ShopsController(ShoppingCenterContext context)
        {
            _context = context;
        }

        // GET: Shops
        public async Task<IActionResult> Index(string shopGenre, string searchString, string shopFloor)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> categoryQuery = from m in _context.Shop
                                            orderby m.ShopCategory
                                            select m.ShopCategory;
            var shops = from m in _context.Shop
                        select m;

            IQueryable<string> floorQuery = from m in _context.Shop
                                            orderby m.Floor.ToString()
                                            select m.Floor.ToString();
            var shopss = from m in _context.Shop
                        select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                shops = shops.Where(s => s.ShopName.ToLower()!.Contains(searchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(shopGenre))
            {
                shops = shops.Where(x => x.ShopCategory == shopGenre);
            }

            if (!string.IsNullOrEmpty(shopFloor))
            {
                shops = shops.Where(x => x.Floor.ToString() == shopFloor);
            }

            var shopGenreVM = new ShopGenreViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Floors = new SelectList(await floorQuery.Distinct().ToListAsync()),
                Shops = await shops.ToListAsync()
            };

            return View(shopGenreVM);
        }

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shop == null)
            {
                return NotFound();
            }

            var shop = await _context.Shop
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShopName,ShopCategory,Floor,OpenHour,CloseHour,OpenInSunday")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shop == null)
            {
                return NotFound();
            }

            var shop = await _context.Shop.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShopName,ShopCategory,Floor,OpenHour,CloseHour,OpenInSunday")] Shop shop)
        {
            if (id != shop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopExists(shop.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shop == null)
            {
                return NotFound();
            }

            var shop = await _context.Shop
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shop == null)
            {
                return Problem("Entity set 'ShoppingCenterContext.Shop'  is null.");
            }
            var shop = await _context.Shop.FindAsync(id);
            if (shop != null)
            {
                _context.Shop.Remove(shop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopExists(int id)
        {
          return (_context.Shop?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
