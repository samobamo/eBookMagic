using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OCR
{
    public partial class ScreenShot2 : Form
    {
        #region Win32 Constants and Imports

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCAPTION = 0x2;

        // Hit-test codes for custom form resizing
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;

        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        #endregion

        #region Constants

        private const int BORDER_THICKNESS = 10;

        #endregion

        #region Fields

        private bool _wasCancelled;

        #endregion

        #region Properties

        public Form InstanceRef { get; set; }

        #endregion

        #region Border Rectangles

        private Rectangle TopBorder => new Rectangle(0, 0, ClientSize.Width, BORDER_THICKNESS);
        private Rectangle LeftBorder => new Rectangle(0, 0, BORDER_THICKNESS, ClientSize.Height);
        private Rectangle RightBorder => new Rectangle(ClientSize.Width - BORDER_THICKNESS, 0, BORDER_THICKNESS, ClientSize.Height);
        private Rectangle BottomBorder => new Rectangle(0, ClientSize.Height - BORDER_THICKNESS, ClientSize.Width, BORDER_THICKNESS);

        private Rectangle TopLeftCorner => new Rectangle(0, 0, BORDER_THICKNESS, BORDER_THICKNESS);
        private Rectangle TopRightCorner => new Rectangle(ClientSize.Width - BORDER_THICKNESS, 0, BORDER_THICKNESS, BORDER_THICKNESS);
        private Rectangle BottomLeftCorner => new Rectangle(0, ClientSize.Height - BORDER_THICKNESS, BORDER_THICKNESS, BORDER_THICKNESS);
        private Rectangle BottomRightCorner => new Rectangle(ClientSize.Width - BORDER_THICKNESS, ClientSize.Height - BORDER_THICKNESS, BORDER_THICKNESS, BORDER_THICKNESS);

        #endregion

        #region Constructor

        public ScreenShot2()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
        

        #endregion

        #region Public API

        public async Task<SelectionResult> GetSelectionResultAsync()
        {
            // Allow time for form to disappear before screenshot (configurable)
            await Task.Delay(AppConfig.FormCloseDelayMs);
            
            var bounds = new Rectangle(Location.X, Location.Y, Width, Height);
            return new SelectionResult(bounds, _wasCancelled);
        }        

        #endregion

        #region Event Handlers

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _wasCancelled = false;
            Close();
        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Red, TopBorder);
            e.Graphics.FillRectangle(Brushes.Red, LeftBorder);
            e.Graphics.FillRectangle(Brushes.Red, RightBorder);
            e.Graphics.FillRectangle(Brushes.Red, BottomBorder);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                _wasCancelled = true;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST)
            {
                var cursor = PointToClient(Cursor.Position);

                if (TopLeftCorner.Contains(cursor))
                    message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRightCorner.Contains(cursor))
                    message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeftCorner.Contains(cursor))
                    message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRightCorner.Contains(cursor))
                    message.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (TopBorder.Contains(cursor))
                    message.Result = (IntPtr)HTTOP;
                else if (LeftBorder.Contains(cursor))
                    message.Result = (IntPtr)HTLEFT;
                else if (RightBorder.Contains(cursor))
                    message.Result = (IntPtr)HTRIGHT;
                else if (BottomBorder.Contains(cursor))
                    message.Result = (IntPtr)HTBOTTOM;
            }
        }

        #endregion

        #region Nested Types

        public class SelectionResult
        {
            public Rectangle Bounds { get; }
            public bool WasCancelled { get; }

            public SelectionResult(Rectangle bounds, bool wasCancelled)
            {
                Bounds = bounds;
                WasCancelled = wasCancelled;
            }
        }

        #endregion
    }
}
