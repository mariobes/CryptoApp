using CryptoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp.Data
{
    public class TransactionEFRepository : ITransactionRepository
    {
        private readonly CryptoAppContext _context;

        public TransactionEFRepository(CryptoAppContext context)
        {
            _context = context;
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            SaveChanges();
        }

        public IEnumerable<Transaction> GetAllTransactionsAllUsers() 
        {
            var query = _context.Transactions.AsQueryable();

            var result = query.ToList();
            return result;
        }

        public IEnumerable<Transaction> GetAllTransactions(int userId, TransactionQueryParameters transactionQueryParameters) 
        {
            var query = _context.Transactions.AsQueryable();

            query = _context.Transactions.Where(t => t.UserId == userId);

            if (transactionQueryParameters.CryptoId.HasValue)
            {
                query = query.Where(t => t.CryptoId == transactionQueryParameters.CryptoId);
            }

            if (transactionQueryParameters.FromDate.HasValue)
            {
                query = query.Where(t => t.Date >= transactionQueryParameters.FromDate);
            }

            if (transactionQueryParameters.ToDate.HasValue)
            {
                query = query.Where(t => t.Date <= transactionQueryParameters.ToDate);
            }

            if (!string.IsNullOrWhiteSpace(transactionQueryParameters.Concept))
            {
                query = query.Where(t => t.Concept.StartsWith(transactionQueryParameters.Concept));
            }

            switch (transactionQueryParameters.SortBy)
            {
                case "amount":
                    query = transactionQueryParameters.Order == "asc" ? query.OrderByDescending(t => t.Amount) : query.OrderBy(t => t.Amount);
                    break;
                case "concept":
                    query = transactionQueryParameters.Order == "asc" ? query.OrderBy(t => t.Concept) : query.OrderByDescending(t => t.Concept);
                    break;
                case "date":
                default:
                    query = transactionQueryParameters.Order == "asc" ? query.OrderBy(t => t.Date) : query.OrderByDescending(t => t.Date);
                    break;
            }

            var result = query.ToList();
            return result;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }   
}