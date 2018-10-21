using System.Windows.Controls;
using System.Windows.Media;

namespace ScoutingApp
{
    public partial class MsgLabel : UserControl
    {
        private readonly Color Error = Colors.OrangeRed;
        private readonly Color Message = Colors.LightGreen;

        public MsgLabel()
        {
            InitializeComponent();
        }

        public void ShowMessage(string msg, bool isErr)
        {
            Label.Foreground = isErr ? new SolidColorBrush(Error) : new SolidColorBrush(Message);
            Label.Content = msg;
        }
        
    }
}
