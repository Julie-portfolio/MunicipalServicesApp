using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MunicipalServicesApp.Forms
{
    // Static painter helper to draw the neon background and framed panel.
    public static class NeonPainter
    {
        public static void PaintBackground(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // full window gradient
            using (var brush = new LinearGradientBrush(rect, Color.FromArgb(12, 18, 28), Color.FromArgb(6, 12, 22), LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            // inner panel
            int margin = 24;
            var inner = new Rectangle(margin, margin + 8, rect.Width - margin * 2, rect.Height - margin * 2 - 8);

            // glow strokes
            for (int i = 0; i < 6; i++)
            {
                int expand = i * 2;
                var glowRect = Rectangle.Inflate(inner, expand, expand);
                int alpha = Math.Max(0, 80 - i * 12);
                using (var pen = new Pen(Color.FromArgb(alpha, 6, 193, 196), 2))
                {
                    using (var gp = RoundedRect(glowRect, 12 + i))
                    {
                        g.DrawPath(pen, gp);
                    }
                }
            }

            // inner panel background
            using (var gp = RoundedRect(inner, 12))
            using (var brush = new LinearGradientBrush(inner, Color.FromArgb(16, 30, 45), Color.FromArgb(8, 14, 26), LinearGradientMode.Vertical))
            {
                g.FillPath(brush, gp);
                using (var pen = new Pen(Color.FromArgb(80, 40, 180, 220), 1))
                {
                    g.DrawPath(pen, gp);
                }
            }
        }

        public static GraphicsPath RoundedRect(Rectangle r, int radius)
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
