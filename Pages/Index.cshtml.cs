using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ContactToolApp.Models;
using ContactToolApp.ViewModels;
using ContactToolApp.Services;
using MongoDB.Bson;

namespace ContactToolApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Contacts _contacts;

        public IEnumerable<Contact> ContactList;

        [BindProperty]
        public ContactForm ContactForm { get; set; }

        public IndexModel(
          ILogger<IndexModel> logger,
          Contacts contacts
        )
        {
          _contacts = contacts;
          _logger = logger;
        }

        public async Task OnGetAsync()
        {
          ContactList = await _contacts.All();
          System.Console.WriteLine("Contact List Count: {0}", ContactList.Count());
        }

        public async Task<IActionResult> OnPostAppendAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Contact contact = new Contact() {
              FirstName = ContactForm.FirstName,
              LastName = ContactForm.LastName,
              Phone = ContactForm.Phone,
              Email = ContactForm.Email,
            };

            await _contacts.Append(contact);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string contactId)
        {
            await _contacts.Delete(new ObjectId(contactId));

            return RedirectToPage("./Index");
        }        
    }
}
