using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Data.Entities
{
    public class Cliente : INotifyPropertyChanged
    {
        public int IdCliente { get; set;}
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
