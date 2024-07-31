using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using sem2.riverStations.urbanec.ait4.Services;

namespace sem2.riverStations.urbanec.ait4.Pages
{
    public class TokenChangerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string textInput { get; set; }

        public string HashResult { get; private set; }

        public List<TokenSetUp> Tokens { get; set; }

        public TokenChangerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Tokens = _context.TokenSetUp.ToList();
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }


        public IActionResult OnPostDelete(string token)
        {
            var tokenToDelete = _context.TokenSetUp.FirstOrDefault(t => t.Token == token);
            if (tokenToDelete != null)
            {
                _context.TokenSetUp.Remove(tokenToDelete);
                _context.SaveChanges();
            }

            return RedirectToPage(); // Redirect to the same page after deletion
        }


        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(textInput))
            {
                HashResult = GetMd5Hash(textInput);

                // Save the token to the database
                var token = new TokenSetUp { Token = HashResult };
                _context.TokenSetUp.Add(token);
                _context.SaveChanges();

                // Refresh the tokens list
                Tokens = _context.TokenSetUp.ToList();
            }
            else
            {
                textInput = "You haven't entered any text";
            }
            return Page();
        }

        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
