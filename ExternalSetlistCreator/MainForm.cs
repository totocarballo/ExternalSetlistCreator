using System.Collections.Concurrent;

namespace ExternalSetlistCreator
{
    public partial class MainForm : Form
    {
        private const string? Title = "External Setlist Creator";

        public MainForm()
        {
            Icon = Resources.icon;
            InitializeComponent();
        }

        private void BtnAddFile_Click(object sender, EventArgs e)
        {
            var result = OfdAdd.ShowDialog();

            if (!result.Equals(DialogResult.OK))
            {
                return;
            }

            if (LstFiles.Items.Contains(OfdAdd.FileName))
            {
                MessageBox.Show(this, "Chart is already in the list.", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var file = Path.GetFileName(OfdAdd.FileName);

            if (file.Equals("notes.chart") || file.Equals("notes.mid"))
            {
                LstFiles.Items.Add($"{OfdAdd.FileName} [{NumSpeed.Value}%]");
            }
            else
            {
                MessageBox.Show(this, $"The current file is not compatible:\n{OfdAdd.FileName}.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateTitle();
        }

        private void BtnAddFolder_Click(object sender, EventArgs e)
        {
            var result = FbdAdd.ShowDialog();

            if (result != DialogResult.OK)
                return;

            var speed = (int)NumSpeed.Value;

            var path = FbdAdd.SelectedPath;
            var list = new ConcurrentBag<string>();

            var files = Directory.GetFiles(path, "notes.chart", SearchOption.AllDirectories)
                                 .Concat(Directory.GetFiles(path, "notes.mid", SearchOption.AllDirectories));

            Parallel.ForEach(files, (file) =>
            {
                if (file != null)
                {
                    lock (list)
                    {
                        if (!list.Contains(file) && !LstFiles.Items.Contains(file))
                        {
                            list.Add($"{file} [{speed}%]");
                        }
                    }
                }
            });

            LstFiles.Items.AddRange(list.OrderBy(f => f).ToArray());
            UpdateTitle();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            LstFiles.Items.Clear();
            Text = Title;
        }

        private void BtnExportCh_Click(object sender, EventArgs e)
        {
            ExportSetlist(false);
        }

        private void BtnExportYarg_Click(object sender, EventArgs e)
        {
            ExportSetlist(true);
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            int nuevoNumero = (int)NumSpeed.Value;

            var modifiedIndices = new HashSet<int>();

            foreach (var selectedItem in LstFiles.SelectedItems.Cast<string>().ToList())
            {
                for (var i = 0; i < LstFiles.Items.Count; i++)
                {
                    if (LstFiles.Items[i].ToString() == selectedItem)
                    {
                        string? elemento = LstFiles.Items[i].ToString();

                        if (string.IsNullOrEmpty(elemento))
                        {
                            continue;
                        }

                        int inicioNumero = elemento.LastIndexOf('[');
                        int finNumero = elemento.LastIndexOf(']');

                        if (inicioNumero != -1 && finNumero != -1 && inicioNumero < finNumero)
                        {
                            var output = $"{nuevoNumero}%";

                            LstFiles.Items[i] = string.Concat(elemento.AsSpan(0, inicioNumero + 1), output, elemento.AsSpan(finNumero));

                            modifiedIndices.Add(i);
                        }

                        break;
                    }
                }
            }

            LstFiles.BeginUpdate();

            LstFiles.ClearSelected();

            foreach (var index in modifiedIndices)
            {
                if (index >= 0 && index < LstFiles.Items.Count)
                {
                    LstFiles.SetSelected(index, true);
                }
            }

            LstFiles.EndUpdate();
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(1);
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(-1);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            int selectedIndex = LstFiles.SelectedIndex;

            if (LstFiles.SelectedItem != null)
            {
                LstFiles.Items.Remove(LstFiles.SelectedItem);
            }

            if (LstFiles.Items.Count > 0)
            {
                if (selectedIndex >= LstFiles.Items.Count)
                {
                    LstFiles.SelectedIndex = LstFiles.Items.Count - 1;
                }
                else
                {
                    LstFiles.SelectedIndex = selectedIndex;
                }

                UpdateTitle();
            }
            else
            {
                Text = Title;
            }
        }

        private void ChkSortList_CheckedChanged(object sender, EventArgs e)
        {
            LstFiles.Sorted = ChkSortList.Checked;
        }

        private void ExportSetlist(bool json)
        {
            if (LstFiles.Items.Count == 0)
            {
                MessageBox.Show(this, "Add files before exporting", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result;

            if (json)
            {
                MessageBox.Show(this, "YARG currently only supports overwriting the 'Favorite' playlist and does not support setting custom speed per song.\n\nPlease make a backup to preserve your list of favorite songs.\n\nThe game MUST BE CLOSED before continuing.\nYou can reopen it after the playlist is exported.", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = FbdExport.ShowDialog();
            }
            else
            {
                result = SfdExport.ShowDialog();
            }

            if (result.Equals(DialogResult.OK))
            {
                var items = new List<string>();

                foreach (string item in LstFiles.Items)
                {
                    items.Add(item);
                }

                if (json)
                {
                    Export.Playlist(FbdExport.SelectedPath, items);
                }
                else
                {
                    Export.Setlist(SfdExport.FileName, items.Count, items);
                }

                MessageBox.Show(this, "Setlist exported!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LstFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowInputBox();
            }
        }

        private void LstFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInputBox();
        }

        private void LstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstFiles.SelectedItem != null)
            {
                TipMessage.SetToolTip(LstFiles, LstFiles.SelectedItem.ToString());
            }
        }

        private void MoverItemToPosition(int posicionDeseada)
        {
            if (LstFiles.SelectedIndex < 0)
            {
                return;
            }

            if (posicionDeseada < 0 || posicionDeseada >= LstFiles.Items.Count)
            {
                return;
            }

            if (LstFiles.SelectedItem == null)
            {
                return;
            }

            object itemSeleccionado = LstFiles.SelectedItem;

            LstFiles.Items.Remove(itemSeleccionado);
            LstFiles.Items.Insert(posicionDeseada, itemSeleccionado);
            LstFiles.SelectedIndex = posicionDeseada;
        }

        private void MoveSelectedItems(int direction)
        {
            var selectedIndices = LstFiles.SelectedIndices;

            List<int> listIndices = selectedIndices.Cast<int>().ToList();

            listIndices.Sort();

            int amountSelected = listIndices.Count;

            if (amountSelected == 0)
            {
                return;
            }

            if ((listIndices.FirstOrDefault() == 0 && direction == -1) || (listIndices.LastOrDefault() == LstFiles.Items.Count - 1 && direction == 1))
            {
                return;
            }

            int amountMove = direction;

            bool isShift = ModifierKeys.HasFlag(Keys.Shift);

            if (isShift)
            {
                amountMove = direction == -1 ? -listIndices[0] : LstFiles.Items.Count - 1 - listIndices.Max();
            }

            List<object> selectedElements = new();

            foreach (int indice in listIndices)
            {
                selectedElements.Add(LstFiles.Items[indice]);
            }

            foreach (object item in selectedElements)
            {
                LstFiles.Items.Remove(item);
            }

            for (int i = 0; i < amountSelected; i++)
            {
                int newIndex = listIndices[i] + amountMove;

                if (newIndex < 0)
                {
                    newIndex = 0;
                }
                else if (newIndex > LstFiles.Items.Count)
                {
                    newIndex = LstFiles.Items.Count;
                }

                LstFiles.Items.Insert(newIndex, selectedElements[i]);
            }

            LstFiles.ClearSelected();
            foreach (int newIndex in listIndices.Select(i => i + amountMove))
            {
                if (newIndex >= 0 && newIndex < LstFiles.Items.Count)
                {
                    LstFiles.SetSelected(newIndex, true);
                }
            }
        }

        private void NumSpeed_MouseClick(object sender, MouseEventArgs e)
        {
            NumSpeed.Select(0, NumSpeed.Text.Length);
        }

        private void NumSpeed_ValueChanged(object sender, EventArgs e)
        {
            int valorInicial = (int)NumSpeed.Value;
            NumSpeed.Value = (int)Math.Round(valorInicial / 5.0) * 5;
        }

        private void SearchInList(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText) || LstFiles.Items.Count == 0)
            {
                //LstFiles.ClearSelected();
                return;
            }

            for (int i = 0; i < LstFiles.Items.Count; i++)
            {
                string? listText = LstFiles.Items[i].ToString();

                if (string.IsNullOrWhiteSpace(listText))
                {
                    return;
                }

                LstFiles.SelectedItems.Clear();

                if (listText.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    LstFiles.SelectedIndex = i;
                    return;
                }
            }

            LstFiles.ClearSelected();
        }

        private bool ShowInputBox()
        {
            if (LstFiles.SelectedItem != null)
            {
                if (LstFiles.SelectedIndices.Count >= 1)
                {
                    var index = LstFiles.SelectedIndices[0];
                    LstFiles.ClearSelected();
                    LstFiles.SetSelected(index, true);
                }

                TipMessage.SetToolTip(LstFiles, "");

                string? item = LstFiles.SelectedItem.ToString();

                var pos = LstFiles.SelectedIndex + 1;

                var result = InputBox.Show(item!, $"Select a position between 1 (Top) or {LstFiles.Items.Count} (Bottom)", ref pos);

                if (result == DialogResult.OK)
                {
                    MoverItemToPosition(pos - 1);
                    return true;
                }
            }
            return false;
        }
        private void TxtSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.SelectAll();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ShowInputBox())
            {
                TxtSearch.Text = "";
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchInList(TxtSearch.Text);
        }

        private void UpdateTitle()
        {
            Text = $"{Title} - {LstFiles.Items.Count} Songs";
        }
    }
}