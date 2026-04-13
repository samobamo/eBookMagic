using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR
{    
    public class ImageTextBuffer
    {
        public int PageNumber { get; set; }
        public string Text { get; set; }                
        public Image Image { get; set; } = null;
        public ImageTextBuffer(int PageNumber, string Text, Image Image = null)
        {                        
            this.PageNumber = PageNumber;
            this.Text = Text;
            this.Image = Image;
        }
    }
}