using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IUsuario
    {
        public ML.Result GetAll(ML.Usuario usuario);
        public ML.Result Delete(int IdUsuario);
        public ML.Result GetById(int IdUsuario);
        public ML.Result Add(ML.Usuario usuario);
        public ML.Result Update(ML.Usuario usuario);
    }
}
