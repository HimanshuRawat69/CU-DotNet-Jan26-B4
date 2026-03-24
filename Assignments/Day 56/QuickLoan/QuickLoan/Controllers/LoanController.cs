using Microsoft.AspNetCore.Mvc;
using QuickLoan.Models;

namespace QuickLoan.Controllers
{
    public class LoanController : Controller
    {
        private static List

        <Loan> loans = new List<Loan>();

        public IActionResult Index()
        {
            return View(loans);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Id = loans.Any() ? loans.Max(x => x.Id) + 1 : 1;
                loans.Add(loan);
                return RedirectToAction("Index");
            }

            return View(loan);
        }

        public IActionResult Edit(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }

        [HttpPost]
        public IActionResult Edit(Loan loan)
        {
            if (!ModelState.IsValid)
                return View(loan);

            var existing = loans.FirstOrDefault(x => x.Id == loan.Id);

            if (existing == null)
                return NotFound();

            existing.BorrowerName = loan.BorrowerName;
            existing.LenderName = loan.LenderName;
            existing.Amount = loan.Amount;
            existing.IsSettled = loan.IsSettled;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if (loan != null)
            {
                loans.Remove(loan);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if (loan != null)
                loans.Remove(loan);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }
    }
}