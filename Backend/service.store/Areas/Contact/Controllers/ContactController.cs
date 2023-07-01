using App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace App.Areas.Contacts.Controllers
{
      [Authorize(Roles = RoleNames.Administrator)]
      [Area("Contact")]
      [Route("Contact/[action]")]
      [ApiExplorerSettings(IgnoreApi = true)]
      public class ContactController : Controller
      {
            private readonly DataContext _context;

            public ContactController(DataContext context)
            {
                  _context = context;
            }

            // GET: Contact
            public async Task<IActionResult> Index()
            {
                  return _context.Contacts != null ?
                              View(await _context.Contacts.ToListAsync()) :
                              Problem("Entity set 'DataContext.Contacts'  is null.");
            }

            // GET: Contact/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                  if (id == null || _context.Contacts == null)
                  {
                        return NotFound();
                  }

                  var contact = await _context.Contacts
                        .FirstOrDefaultAsync(m => m.Id == id);
                  if (contact == null)
                  {
                        return NotFound();
                  }

                  return View(contact);
            }
            [AllowAnonymous]
            // GET: Contact/Create
            public IActionResult Create()
            {
                  return View();
            }
            [TempData]
            public string Message { get; set; } = string.Empty;

            // POST: Contact/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [AllowAnonymous]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,FullName,Email,Phone,Message,SentAt")] Contact.Models.Contact contact)
            {
                  if (ModelState.IsValid)
                  {
                        contact.SentAt = DateTime.UtcNow;
                        _context.Add(contact);
                        await _context.SaveChangesAsync();
                        Message = "Sent contact successfully!";
                        return RedirectToAction("index", "home");
                  }
                  return View(contact);
            }

            // GET: Contact/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                  if (id == null || _context.Contacts == null)
                  {
                        return NotFound();
                  }

                  var contact = await _context.Contacts.FindAsync(id);
                  if (contact == null)
                  {
                        return NotFound();
                  }
                  return View(contact);
            }

            // POST: Contact/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Phone,Message,SentAt")] Contact.Models.Contact contact)
            {
                  if (id != contact.Id)
                  {
                        return NotFound();
                  }

                  if (ModelState.IsValid)
                  {
                        try
                        {
                              _context.Update(contact);
                              await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                              if (!ContactExists(contact.Id))
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
                  return View(contact);
            }

            // GET: Contact/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                  if (id == null || _context.Contacts == null)
                  {
                        return NotFound();
                  }

                  var contact = await _context.Contacts
                      .FirstOrDefaultAsync(m => m.Id == id);
                  if (contact == null)
                  {
                        return NotFound();
                  }

                  return View(contact);
            }

            // POST: Contact/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                  if (_context.Contacts == null)
                  {
                        return Problem("Entity set 'DataContext.Contacts'  is null.");
                  }
                  var contact = await _context.Contacts.FindAsync(id);
                  if (contact != null)
                  {
                        _context.Contacts.Remove(contact);
                  }

                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
            }

            private bool ContactExists(int id)
            {
                  return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
            }
      }
}

