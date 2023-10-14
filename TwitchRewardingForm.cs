using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchRewardingEngine;

namespace TwitchRewarding
{
    public partial class TwitchRewardingForm : Form
    {
        TwitchRewardParameters p;
        Dictionary<string, TwitchViewersRewards> ViewersRewards = TVR.LoadViewerStates();
        BindingList<TwitchViewersRewards> BL_ViewersRewards;
        public TwitchRewardingForm()
        {
            InitializeComponent();
            p = TwitchRewardParameters.Load();

            c_Host.DataBindings.Add(new Binding("Value", p, c_Host.Name.Substring(2)));
            c_Raid.DataBindings.Add(new Binding("Value", p, c_Raid.Name.Substring(2)));
            c_Follow.DataBindings.Add(new Binding("Value", p, c_Follow.Name.Substring(2)));
            c_SubPrime.DataBindings.Add(new Binding("Value", p, c_SubPrime.Name.Substring(2)));
            c_SubT1.DataBindings.Add(new Binding("Value", p, c_SubT1.Name.Substring(2)));
            c_SubT2.DataBindings.Add(new Binding("Value", p, c_SubT2.Name.Substring(2)));
            c_SubT3.DataBindings.Add(new Binding("Value", p, c_SubT3.Name.Substring(2)));
            c_GiftSubT1.DataBindings.Add(new Binding("Value", p, c_GiftSubT1.Name.Substring(2)));
            c_GiftSubT2.DataBindings.Add(new Binding("Value", p, c_GiftSubT2.Name.Substring(2)));
            c_GiftSubT3.DataBindings.Add(new Binding("Value", p, c_GiftSubT3.Name.Substring(2)));
            c_TipPerUnit.DataBindings.Add(new Binding("Value", p, c_TipPerUnit.Name.Substring(2)));
            c_PresentPer5minutes.DataBindings.Add(new Binding("Value", p, c_PresentPer5minutes.Name.Substring(2)));
            c_MessageSent.DataBindings.Add(new Binding("Value", p, c_MessageSent.Name.Substring(2)));
            c_FirstMessageBonus.DataBindings.Add(new Binding("Value", p, c_FirstMessageBonus.Name.Substring(2)));
            c_CheerPerUnit.DataBindings.Add(new Binding("Value", p, c_CheerPerUnit.Name.Substring(2)));
            c_ChannelPointRewardPerPoint.DataBindings.Add(new Binding("Value", p, c_ChannelPointRewardPerPoint.Name.Substring(2)));
            c_TimeOutMalus.DataBindings.Add(new Binding("Value", p, c_TimeOutMalus.Name.Substring(2)));

            c_DetailedHistory.DataBindings.Add(new Binding("Checked", p, c_DetailedHistory.Name.Substring(2)));
            c_MoneySymbol.DataBindings.Add(new Binding("Text", p, c_MoneySymbol.Name.Substring(2)));
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            p.Save();
        }

        private void LoadViewersData_Click(object sender, EventArgs e)
        {
            ViewersRewards = TVR.LoadViewerStates();
            BL_ViewersRewards = new BindingList<TwitchViewersRewards>();
            foreach(KeyValuePair<string, TwitchViewersRewards> kvp in ViewersRewards.OrderBy(x => x.Key)) { BL_ViewersRewards.Add(kvp.Value); }
            dataGridView1.DataSource = BL_ViewersRewards;
            Count.Text = $"Viewers : {BL_ViewersRewards.Count}";
            foreach(DataGridViewColumn c in dataGridView1.Columns) { c.SortMode = DataGridViewColumnSortMode.Automatic; }
        }

        private void SaveViewersData_Click(object sender, EventArgs e)
        {
            ViewersRewards = new Dictionary<string, TwitchViewersRewards>();
            foreach (TwitchViewersRewards tvr in BL_ViewersRewards) { ViewersRewards.Add(tvr.TwitchUserName, tvr); }
            TVR.SaveViewersState(ViewersRewards);
        }

        private void MergeDuiplicates_Click(object sender, EventArgs e)
        {
            Dictionary<string, TwitchViewersRewards> Viewers = TVR.LoadViewerStates();
            Dictionary<string, TwitchViewersRewards> ViewersNoDuplicates = new Dictionary<string, TwitchViewersRewards>();

            foreach (KeyValuePair<string, TwitchViewersRewards> v in Viewers)
            {
                long i = ViewersNoDuplicates.LongCount(x => x.Key.ToLower() == v.Key.ToLower());
                if (i == 0)
                {
                    ViewersNoDuplicates.Add(v.Key, v.Value);
                }
                else if (i == 1)
                {
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.Points += v.Value.Points;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.nbTip += v.Value.nbTip;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.TotalTip += v.Value.TotalTip;

                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.Host += v.Value.Host;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.Raid += v.Value.Raid;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.Follow += v.Value.Follow;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.SubPrime += v.Value.SubPrime;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.SubT1 += v.Value.SubT1;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.SubT2 += v.Value.SubT2;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.SubT3 += v.Value.SubT3;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.GiftSubT1 += v.Value.GiftSubT1;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.GiftSubT2 += v.Value.GiftSubT2;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.GiftSubT3 += v.Value.GiftSubT3;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.TipPerUnit += v.Value.TipPerUnit;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.PresentPer5minutes += v.Value.PresentPer5minutes;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.MessageSent += v.Value.MessageSent;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.FirstMessageBonus += v.Value.FirstMessageBonus;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.CheerPerUnit += v.Value.CheerPerUnit;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.ChannelPointRewardPerPoint += v.Value.ChannelPointRewardPerPoint;
                    ViewersNoDuplicates.First(x => x.Key.ToLower() == v.Key.ToLower()).Value.TimeOutMalus += v.Value.TimeOutMalus;
                }
                else { Console.WriteLine($"{v.Key} failed to be merged."); }
            }

            TVR.SaveViewersState(ViewersNoDuplicates);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Merge2.Enabled = dataGridView1.SelectedRows.Count == 2 ? true : false;
        }

        private void Merge2_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewCell c in dataGridView1.SelectedRows[0].Cells)
            {
                if (c.Value.GetType() == typeof(int))
                {
                    int t = (int)dataGridView1.SelectedRows[1].Cells[c.ColumnIndex].Value;
                    c.Value = (int)c.Value + t;
                }
                else if (c.Value.GetType() == typeof(decimal))
                {
                    decimal t = (decimal)dataGridView1.SelectedRows[1].Cells[c.ColumnIndex].Value;
                    c.Value = (decimal)c.Value + t;
                }
            }

            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[1].Index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewersRewards = TVR.LoadViewerStates();
            BL_ViewersRewards = new BindingList<TwitchViewersRewards>();
            foreach (KeyValuePair<string, TwitchViewersRewards> kvp in ViewersRewards.OrderByDescending(x => x.Value.Points)) { BL_ViewersRewards.Add(kvp.Value); }
            dataGridView2.DataSource = BL_ViewersRewards;
            Count.Text = $"Viewers : {BL_ViewersRewards.Count}";
            foreach (DataGridViewColumn c in dataGridView2.Columns) { c.SortMode = DataGridViewColumnSortMode.Automatic; }

            int i = 1;
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (i <= 5) { r.DefaultCellStyle.BackColor = Color.LightYellow; }
                if (i > 5 && i <= 10) { r.DefaultCellStyle.BackColor = Color.LightCyan; }
                if (i > 10 && i <= 20) { r.DefaultCellStyle.BackColor = Color.LightGray; }
                if (i > 20) break;
                i++;
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
            Dictionary<string, TwitchViewersRewards> t = TVR.LoadViewerStates();
            t.Reward("Akuaina", "StreamerBonus", 1);
            TVR.SaveViewersState(t);
        }
    }
}
