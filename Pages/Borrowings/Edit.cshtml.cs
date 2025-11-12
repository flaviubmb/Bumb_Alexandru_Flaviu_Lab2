using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bumb_Alexandru_Flaviu_Lab2.Data;
using Bumb_Alexandru_Flaviu_Lab2.Models;

namespace Bumb_Alexandru_Flaviu_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Bumb_Alexandru_Flaviu_Lab2.Data.Bumb_Alexandru_Flaviu_Lab2Context _context;

        public EditModel(Bumb_Alexandru_Flaviu_Lab2.Data.Bumb_Alexandru_Flaviu_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing == null)
            {
                return NotFound();
            }

            Borrowing = borrowing;


            ViewData["BookID"] = new SelectList(
                _context.Book.Include(b => b.Author).Select(b => new {
                    b.ID,
                    DisplayText = b.Title + " - " + b.Author.FullName
                }),
                "ID",
                "DisplayText",
                borrowing.BookID
            );


            ViewData["MemberID"] = new SelectList(
                _context.Member.Select(m => new {
                    m.ID,
                    DisplayText = m.FullName
                }),
                "ID",
                "DisplayText",
                borrowing.MemberID);

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
