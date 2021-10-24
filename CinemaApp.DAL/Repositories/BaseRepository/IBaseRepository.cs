using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.BaseRepository
{
    public interface IBaseRepository<Entity>
    {
        void SaveChanges();
    }
}
