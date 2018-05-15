
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Domain;


namespace MoviesAndMagic.Data.Infrastructure
{
   public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private MoviesAndMagicEntities dataEntities;

        public MoviesAndMagicEntities Get()
        {
            return dataEntities ?? (dataEntities = new MoviesAndMagicEntities());
        }

        protected override void DisposeCore()
        {
            dataEntities?.Dispose();
        }
    }
}

