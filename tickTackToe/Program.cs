using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickTackToe
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var connectForm = new Connection();
            Application.Run(connectForm);
            var game = connectForm.Game;
            if(game==null)
                return;
            Application.Run(game);
        }
    }
}
