using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms.Controls
{
    // A simple owner-drawn glossy rounded button for modern look
    public class GlossyButton : Button
    {
        private Image _iconImage;

        [Category("Appearance")]
        public Image IconImage
        {
            get => _iconImage;
            set { _iconImage = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color StartColor { get; set; } = Color.FromArgb(3, 169, 244);

        [Category("Appearance")]
        public Color EndColor { get; set; } = Color.FromArgb(0, 122, 204);

        [Category("Layout")]
        public int CornerRadius { get; set; } = 12;

        public GlossyButton()
        {
            // Make control double-buffered to reduce flicker
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            Size = new Size(520, 64);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = ClientRectangle;
            rect.Inflate(-1, -1);

            // shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                var shadowRect = new Rectangle(rect.X + 4, rect.Y + 6, rect.Width, rect.Height);
                using (var path = GetRoundedRectPath(shadowRect, CornerRadius))
                    g.FillPath(shadowBrush, path);
            }

            // gradient
            using (var brush = new LinearGradientBrush(rect, StartColor, EndColor, LinearGradientMode.Vertical))
            using (var path = GetRoundedRectPath(rect, CornerRadius))
            {
                g.FillPath(brush, path);

                // glossy highlight
                var glossRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height / 2);
                using (var glossBrush = new LinearGradientBrush(glossRect, Color.FromArgb(160, Color.White), Color.FromArgb(20, Color.White), LinearGradientMode.Vertical))
                using (var glossPath = GetRoundedRectPath(glossRect, CornerRadius))
                {
                    g.FillPath(glossBrush, glossPath);
                }

                // border
                using (var pen = new Pen(Color.FromArgb(100, Color.Black)))
                {
                    g.DrawPath(pen, path);
                }
            }

            // draw icon if set
            int iconArea = 0;
            if (IconImage != null)
            {
                iconArea = rect.Height - 16;
                var imgRect = new Rectangle(rect.X + 16, rect.Y + 8, iconArea, iconArea);
                g.DrawImage(IconImage, imgRect);
            }

            // draw text
            var textRect = new Rectangle(rect.X + 16 + iconArea + (iconArea > 0 ? 12 : 0), rect.Y, rect.Width - (16 + iconArea + 12), rect.Height);
            TextRenderer.DrawText(g, Text, Font, textRect, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
