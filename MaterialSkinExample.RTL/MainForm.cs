using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    public partial class MainForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public MainForm(RightToLeft RightToLeft = RightToLeft.Yes) : base(RightToLeft)
        {
            var culture = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            base.RightToLeft = RightToLeft.Yes;
            InitializeComponent();
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            
            materialSkinManager.RightToLeft = RightToLeft;
            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);

            // Add dummy data to the listview
            seedListView();
            materialCheckedListBox1.Items.Add("آیتم ۱", false);
            materialCheckedListBox1.Items.Add("آیتم ۲", true);
            materialCheckedListBox1.Items.Add("آیتم ۳", true);
            materialCheckedListBox1.Items.Add("آیتم ۴", false);
            materialCheckedListBox1.Items.Add("آیتم ۵", true);
            materialCheckedListBox1.Items.Add("آیتم ۶", false);
            materialCheckedListBox1.Items.Add("آیتم ۷", false);


            materialListBoxFormStyle.Clear();
            foreach (var FormStyleItem in Enum.GetNames(typeof(MaterialForm.FormStyles)))
            {
                materialListBoxFormStyle.AddItem(FormStyleItem);
                if (FormStyleItem == this.FormStyle.ToString()) materialListBoxFormStyle.SelectedIndex = materialListBoxFormStyle.Items.Count-1;
            }

            materialListBoxFormStyle.SelectedIndexChanged += (sender, args) =>
            {
                MaterialForm.FormStyles SelectedStyle = (MaterialForm.FormStyles)Enum.Parse(typeof(MaterialForm.FormStyles), args.Text);
                if (this.FormStyle!= SelectedStyle) this.FormStyle = SelectedStyle;
            };

            materialMaskedTextBox1.ValidatingType = typeof(System.Int16);

        }

        private void seedListView()
        {
            //Define
            var data = new[]
            {
                new []{"Lollipop", "392", "0.2", "0"},
                new []{"KitKat", "518", "26.0", "7"},
                new []{"Ice cream sandwich", "237", "9.0", "4.3"},
                new []{"Jelly Bean", "375", "0.0", "0.0"},
                new []{"Honeycomb", "408", "3.2", "6.5"}
            };

            //Add
            foreach (string[] version in data)
            {
                var item = new ListViewItem(version);
                materialListView1.Items.Add(item);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
            updateColor();
        }

        private int colorSchemeIndex;

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2)
                colorSchemeIndex = 0;
            updateColor();
        }

        private void updateColor()
        {
            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal500 : Primary.Indigo500,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal700 : Primary.Indigo700,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal200 : Primary.Indigo100,
                        Accent.Pink200,
                        TextShade.WHITE);
                    break;

                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Green600,
                        Primary.Green700,
                        Primary.Green200,
                        Accent.Red100,
                        TextShade.WHITE);
                    break;

                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.BlueGrey800,
                        Primary.BlueGrey900,
                        Primary.BlueGrey500,
                        Accent.LightBlue200,
                        TextShade.WHITE);
                    break;
            }
            Invalidate();
        }

        private void MaterialButton2_Click(object sender, EventArgs e)
        {
            materialProgressBar1.Value = Math.Min(materialProgressBar1.Value + 10, 100);
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            materialProgressBar1.Value = Math.Max(materialProgressBar1.Value - 10, 0);
        }

        private void materialSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            DrawerUseColors = materialSwitch4.Checked;
        }

        private void MaterialSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            DrawerHighlightWithAccent = materialSwitch5.Checked;
        }

        private void MaterialSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            DrawerBackgroundWithAccent = materialSwitch6.Checked;
        }

        private void materialSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            DrawerShowIconsWhenHidden = materialSwitch8.Checked;
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            var builder = new StringBuilder("گزارش عملیات دسته‌ای:\n\n");
            var random = new Random();
            var result = 0;

            for (int i = 0; i < 200; i++)
            {
                result = random.Next(1000);

                if (result < 950)
                {
                    builder.AppendFormat(" - وظیفه {0}: عملیات با موفقیت انجام شد.\n", i);
                }
                else
                {
                    builder.AppendFormat(" - وظیفه {0}: عملیات انجام نشد! یک خطای خیلی خیلی خیلی خیلی خیلی خیلی خیلی خیلی خیلی خیلی خیلی جدی در این عملیات فرعی رخ داده است. کد خطا این است: {1}).\n", i, result);
                }
            }

            var batchOperationResults = builder.ToString();
            batchOperationResults = "متن ساده";
            var mresult = MaterialMessageBox.Show(batchOperationResults, "عملیات دسته‌ای", MessageBoxButtons.YesNoCancel, FlexibleMaterialForm.ButtonsPosition.Center, RightToLeft.Yes);
            //materialComboBox1.Items.Add("this is a very long string");
        }

        private void materialSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            DrawerAutoShow = materialSwitch9.Checked;
        }

        private void materialTextBox2_LeadingIconClick(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new MaterialSnackBar("آیکون پیشرو کلیک کنید");
            SnackBarMessage.Show(this);

        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new MaterialSnackBar("اسنک بار با موفقیت انجام شد", "حله", true, RightToLeft.Yes);
            SnackBarMessage.Show(this);
        }

        private void materialSwitch10_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.UseAccent = materialSwitch10.Checked;
        }

        private void materialSwitch11_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.UseTallSize = materialSwitch11.Checked;
        }

        private void materialSwitch12_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch12.Checked)
                materialTextBox21.Hint = "متن اشاره";
            else
                materialTextBox21.Hint = "";
        }

        private void materialComboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (materialComboBox7.SelectedIndex == 1)
            //{
            //        materialTextBox21.PrefixSuffix = MaterialTextBox2.PrefixSuffixTypes.Prefix;
            //}
            //else if (materialComboBox7.SelectedIndex == 2)
            //{
            //    materialTextBox21.PrefixSuffix = MaterialTextBox2.PrefixSuffixTypes.Suffix;
            //}
            //else 
            //{
            //    materialTextBox21.PrefixSuffix = MaterialTextBox2.PrefixSuffixTypes.None;
            //}
        }

        private void materialSwitch13_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.UseSystemPasswordChar = materialSwitch13.Checked;

        }

        private void materialSwitch14_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch14.Checked)
                materialTextBox21.LeadingIcon = global::MaterialSkinExample.RTL.Properties.Resources.baseline_fingerprint_black_24dp;
            else
                materialTextBox21.LeadingIcon = null;
        }

        private void materialSwitch15_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch15.Checked)
                materialTextBox21.TrailingIcon = global::MaterialSkinExample.RTL.Properties.Resources.baseline_build_black_24dp;
            else
                materialTextBox21.TrailingIcon = null;
        }

        private void materialTextBox21_LeadingIconClick(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new MaterialSnackBar("آیکون پیشرو کلیک کنید");
            SnackBarMessage.Show(this);
        }

        private void materialTextBox21_TrailingIconClick(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new MaterialSnackBar("نماد دنباله کلیک کنید");
            SnackBarMessage.Show(this);
        }

        private void MsReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            materialCheckbox1.ReadOnly = msReadOnly.Checked;
        }
        
        private void materialButton25_Click(object sender, EventArgs e)
        {
            MaterialDialog materialDialog = new MaterialDialog(this, "عنوان دیالوگ", "دیالوگ ها به کاربران در مورد یک کار اطلاع می دهند و می توانند حاوی اطلاعات حیاتی باشند، نیاز به تصمیم گیری داشته باشند یا چندین کار را شامل شوند.", "حله", true, "انصراف", RightToLeft.Yes);
            DialogResult result = materialDialog.ShowDialog(this);

            MaterialSnackBar SnackBarMessage = new MaterialSnackBar(result.ToString(),750, false, "حله",false, RightToLeft.Yes);
            SnackBarMessage.Show(this);

        }
        
        private void materialSwitch16_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.ShowAssistiveText = materialSwitch16.Checked;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel52_Click(object sender, EventArgs e)
        {

        }

        private void materialTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Move(object sender, EventArgs e)
        {

        }

        private void materialExpansionPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
