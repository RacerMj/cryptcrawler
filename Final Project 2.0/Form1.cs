namespace Final_Project_2._0
{
    /*The type can be Fire, Earth, Water, and Air. Determines the monsters that spawn within each level fire = 0, earth = 1, water = 2, air = 3
     *Chance of treasure is 1-100, representing a percent chance */



    public partial class DungeonForm : Form
    {


        Game G;
        public DungeonForm()
        {
            InitializeComponent();
            G = new Game(this);
        }

        /****************************************************************
         * 
         * Utility methods to give access to form controls
         *
         *
         *
         *****************************************************************/
        public String getInputText() { return this.textBox1.Text.Trim(); }
        public void showText(string s) { textBox2.AppendText(s + System.Environment.NewLine); }
        public void showTextLine(string s) { textBox2.AppendText(s + System.Environment.NewLine); }
 
        public void setButtonState(Boolean b) {this.button1.Enabled = b; }
        public void prepareForInput()
        {
            //Reenables button
            this.textBox1.Text = "";
            this.textBox1.Focus();
            this.button1.Enabled = true;
        }

        public void enterHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                G.doCommand();
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            G.doCommand();
        }
    }
}
