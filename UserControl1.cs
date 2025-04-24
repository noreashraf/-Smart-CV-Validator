using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.IO;

namespace SmartCVValidator
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string phonePattern = @"\+?\d{1,3}?[- .]?\(?\d{1,4}?\)?[- .]?\d{1,9}";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string namePattern = @"([A-Z][a-z]+(?:\s[A-Z][a-z]+)+)";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string passwordPattern = @"^.{6,}$";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string postalCodePattern = @"^\d{5}$";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // استرجاع البيانات من الحقول
            string name = textBox4.Text;
            string email = textBox1.Text;
            string phone = textBox5.Text;
            string password = textBox3.Text;
            string address = textBox2.Text;
            string postal = textBox6.Text;

            // تحقق من صحة كل حقل باستخدام Regex
            bool isValid = true;
            string errorMessage = "";

            // الاسم: يبدأ بحرف كبير وممكن يحتوي على مسافات
            if (!Regex.IsMatch(name, @"^[A-Z][a-zA-Z\s]{2,}$"))
            {
                isValid = false;
                errorMessage += "Invalid name.\n";
                textBox4.BackColor = Color.MistyRose;
            }
            else textBox4.BackColor = Color.White;

            // البريد الإلكتروني
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                isValid = false;
                errorMessage += " Invalid e-mail address.\n";
                textBox1.BackColor = Color.MistyRose;
            }
            else textBox1.BackColor = Color.White;

            // رقم الهاتف (مثلاً رقم مصري)
            if (!Regex.IsMatch(phone, @"^01[0-2,5]{1}[0-9]{8}$"))
            {
                isValid = false;
                errorMessage += " Invalid phone number.\n";
                textBox5.BackColor = Color.MistyRose;
            }
            else textBox5.BackColor = Color.White;

            // الباسورد (على الأقل 6 حروف، رقم وحرف كبير وصغير)
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
            {
                isValid = false;
                errorMessage += "The password is weak.\n";
                textBox3.BackColor = Color.MistyRose;
            }
            else textBox3.BackColor = Color.White;

            // العنوان (نتأكد بس إنه مش فاضي)
            if (string.IsNullOrWhiteSpace(address))
            {
                isValid = false;
                errorMessage += "The address cannot be empty.\n";
                textBox2.BackColor = Color.MistyRose;
            }
            else textBox2.BackColor = Color.White;

            // الرمز البريدي (5 أرقام)
            if (!Regex.IsMatch(postal, @"^\d{5}$"))
            {
                isValid = false;
                errorMessage += "Invalid postal code.\n";
                textBox6.BackColor = Color.MistyRose;
            }
            else textBox6.BackColor = Color.White;

            // عرض النتيجة
            if (isValid)
            {
                label11.ForeColor = Color.Green;
                label11.Text = "All data is correct";
            }
            else
            {
                label11.ForeColor = Color.Red;
                label11.Text = errorMessage;
            }

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cvText = textBox7.Text;

            // استخراج الاسم الكامل (نفترض أنه سطر فيه حرف كبير في البداية لكل كلمة)
            Match nameMatch = Regex.Match(cvText, @"(?m)^[A-Z][a-z]+\s[A-Z][a-z]+");

            // استخراج البريد الإلكتروني
            Match emailMatch = Regex.Match(cvText, @"[\w\.-]+@[\w\.-]+\.\w+");

            // استخراج رقم الهاتف (نمط مصري مثلاً)
            Match phoneMatch = Regex.Match(cvText, @"01[0-2,5]\d{8}");

            // استخراج عدد سنوات الخبرة (مثلاً: "3 years", "5+ years")
            Match experienceMatch = Regex.Match(cvText, @"\b\d+\+?\s+years?\b", RegexOptions.IgnoreCase);

            // استخراج المهارات (C#, SQL, Java, Python...)
            MatchCollection skillsMatches = Regex.Matches(cvText, @"\b(C\#|Java|Python|SQL|HTML|CSS|JavaScript|React|Node|ASP\.NET)\b", RegexOptions.IgnoreCase);

            // تنسيق البيانات المستخرجة
            string result = "";

            result += nameMatch.Success ? $"👤 name : {nameMatch.Value}\n" : "The name does not exist\n";
            result += emailMatch.Success ? $"📧 email : {emailMatch.Value}\n" : " The e-mail does not exist\n";
            result += phoneMatch.Success ? $"📞 phone number : {phoneMatch.Value}\n" : "The phone number does not exist\n";
            result += experienceMatch.Success ? $"experience: {experienceMatch.Value}\n" : "The number of years of experience does not exist \n";

            if (skillsMatches.Count > 0)
            {
                var skills = string.Join(", ", skillsMatches.Cast<Match>().Select(m => m.Value));
                result += $"🛠️ المهارات: {skills}\n";
            }
            else
            {
                result += "Skills don't exist \n";
            }

            label9.Text = result;
            label9.ForeColor = Color.DarkBlue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|CSV File (*.csv)|*.csv";
            saveFileDialog.Title = "Save CV Data";
            saveFileDialog.FileName = "CV_Extracted_Data";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, label9.Text);
                    MessageBox.Show("The data was saved successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Got a mistake while saving:\n" + ex.Message, "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
