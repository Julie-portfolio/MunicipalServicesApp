using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    public enum Theme
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static Theme CurrentTheme { get; private set; } = Theme.Light;

        public static void ToggleTheme(Form target)
        {
            CurrentTheme = CurrentTheme == Theme.Light ? Theme.Dark : Theme.Light;
            ApplyTheme(target, CurrentTheme);
        }

        public static void ApplyTheme(Form target, Theme theme)
        {
            var colors = GetColors(theme);

            target.BackColor = colors.Background;
            target.ForeColor = colors.Foreground;

            // Style top-level controls and nested controls
            foreach (Control c in target.Controls.Cast<Control>())
            {
                ApplyToControlRecursive(c, colors);
            }

            // Special window chrome adjustments
            try { target.Text = target.Text; } catch { }
            // persist choice
            SaveTheme(theme);
        }

        public static void LoadTheme()
        {
            try
            {
                // Use a stable application data folder that is available before Application.Run
                var appData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                var cfgdir = System.IO.Path.Combine(appData, "MunicipalServicesApp");
                var cfg = System.IO.Path.Combine(cfgdir, "theme.txt");
                if (System.IO.File.Exists(cfg))
                {
                    var text = System.IO.File.ReadAllText(cfg).Trim();
                    if (text == "Dark") CurrentTheme = Theme.Dark; else CurrentTheme = Theme.Light;
                }
            }
            catch { /* non-fatal */ }
        }

        private static void SaveTheme(Theme theme)
        {
            try
            {
                var appData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                var cfgdir = System.IO.Path.Combine(appData, "MunicipalServicesApp");
                if (!System.IO.Directory.Exists(cfgdir)) System.IO.Directory.CreateDirectory(cfgdir);
                var cfg = System.IO.Path.Combine(cfgdir, "theme.txt");
                System.IO.File.WriteAllText(cfg, theme == Theme.Dark ? "Dark" : "Light");
            }
            catch { /* best-effort, ignore errors */ }
        }

        private static void ApplyToControlRecursive(Control c, (Color Background, Color Foreground, Color Accent, Color Muted) colors)
        {
            // Buttons
            if (c is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = colors.Accent;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
            }

            // Panels and other containers
            if (c is Panel)
            {
                c.BackColor = colors.Background;
            }

            // Labels, TextBoxes, RichTextBox, ComboBox
            if (c is Label || c is TextBox || c is RichTextBox || c is ComboBox || c is LinkLabel)
            {
                c.ForeColor = colors.Foreground;
                if (c is TextBox || c is RichTextBox)
                    c.BackColor = ControlPaint.Light(colors.Background, 0.02f);
            }

            if (c is ProgressBar pb)
            {
                pb.ForeColor = colors.Accent;
                // ProgressBar visual is limited; keep defaults
            }

            // Recursively apply to children
            foreach (Control child in c.Controls.Cast<Control>())
            {
                ApplyToControlRecursive(child, colors);
            }
        }

        private static (Color Background, Color Foreground, Color Accent, Color Muted) GetColors(Theme theme)
        {
            if (theme == Theme.Dark)
            {
                // Neon dark palette
                return (Color.FromArgb(12, 18, 28), Color.FromArgb(220, 230, 240), Color.FromArgb(6, 193, 196), Color.FromArgb(40, 60, 80));
            }

            // Light theme (keep previous)
            return (Color.White, Color.FromArgb(25,25,25), Color.FromArgb(0, 120, 215), Color.FromArgb(230,230,230));
        }
    }
}
