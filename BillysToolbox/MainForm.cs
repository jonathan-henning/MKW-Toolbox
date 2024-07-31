using BillysToolbox.Editors;
using System.Text;

namespace BillysToolbox
{
    public partial class MainForm : Form
    {
        public Dictionary<string, string> FileTypes = new Dictionary<string, string>()
        {
            { "SZS Files (*.szs)", "*.szs" },
            { "ARC Files (*.arc, *.u8)", "*.arc;*.u8" },
            { "BMM Files (*.bmm)", "*.bmm" },
            { "KMP Files (*.kmp)", "*.kmp" },
        };
        public List<KeyValuePair<string, byte[]>> Clipboard = new List<KeyValuePair<string, byte[]>>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenFileEditor()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            StringBuilder filter = new StringBuilder();
            foreach (KeyValuePair<string, string> type in FileTypes)
            {
                if (filter.ToString().CompareTo("") != 0)
                    filter.Append("|");

                filter.Append(type.Key);
                filter.Append("|");
                filter.Append(type.Value);
            }
            ofd.Filter = filter.ToString();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Form? editor = EditorFactory.GetEditor(ofd.FileName);
                if (editor != null)
                {
                    editor.Show();
                    editor.MdiParent = this;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileEditor();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileEditor();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEditor? activeEditor = (IEditor?)ActiveMdiChild;
            if (activeEditor == null) return;

            activeEditor.SaveAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEditor? activeEditor = (IEditor?)ActiveMdiChild;
            if (activeEditor == null) return;

            activeEditor.Save();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            IEditor? activeEditor = (IEditor?)ActiveMdiChild;
            if (activeEditor == null) return;

            activeEditor.Save();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            IEditor? activeEditor = (IEditor?)ActiveMdiChild;
            if (activeEditor == null) return;

            activeEditor.SaveAs();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                Form? editor = EditorFactory.GetEditor(args[1]);
                if(editor == null)
                {
                    MessageBox.Show("Unsupported file type!");
                    return;
                }

                editor.MdiParent    = this;
                editor.WindowState  = FormWindowState.Maximized;
                editor.Show();
            }
        }
    }
}