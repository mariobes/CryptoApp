using CryptoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp.Data
{
    public class CryptoEFRepository : ICryptoRepository
    {
        private readonly CryptoAppContext _context;

        public CryptoEFRepository(CryptoAppContext context)
        {
            _context = context;
        }

        public void AddCrypto(Crypto crypto)
        {
            _context.Cryptos.Add(crypto);
            SaveChanges();
        }

        public IEnumerable<Crypto> GetAllCryptos() 
        {
            var cryptos = _context.Cryptos;
            return cryptos;
        }

        public Crypto GetCrypto(int cryptoId)
        {
            var crypto = _context.Cryptos.FirstOrDefault(crypto => crypto.Id == cryptoId);
            return crypto;
            //.Include(u => u.Transactions).ToList();     Habra que poner esto para que no de null, probablemente al debugear funcionara, y al devolverlo con la api dara eror de referencia circular, habra que utilizar DTOs
        }

        public void UpdateCrypto(Crypto crypto)
        {
            _context.Entry(crypto).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteCrypto(int cryptoId) {
            var crypto = GetCrypto(cryptoId);
            _context.Cryptos.Remove(crypto);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
 
    }   
}