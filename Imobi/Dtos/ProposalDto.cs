using System.Collections.Generic;
using Xamarin.Forms;

namespace Imobi.Dtos
{
    public class ProposalDto
    {
        public int Number { get; set; }
        public string Client { get; set; }
        public string Venture { get; set; }
        public string Tower { get; set; }
        public string Unity { get; set; }
        public Conclusion Conclusion { get; set; }
        public string PenultimateMovement => Movements[1];
        public string LastMovement => Movements[0];

        public IList<string> Movements { get; set; }
    }

    public class Conclusion
    {
        public Color GetColor
        {
            get
            {
                if (PercentageCompletion <= 49) return Color.FromHex("fc314b");
                if (PercentageCompletion >= 50 && PercentageCompletion <= 65) return Color.FromHex("ffb53e");
                if (PercentageCompletion >= 66 && PercentageCompletion <= 90) return Color.FromHex("1ebfae");
                if (PercentageCompletion >= 91 && PercentageCompletion <= 100) return Color.FromHex("219154");

                return Color.Black;
            }
        }

        public int PercentageCompletion { get; set; }
    }
}