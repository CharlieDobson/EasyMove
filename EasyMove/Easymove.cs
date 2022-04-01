using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyMove
{
    public partial class EasyMoveMainForm : Form
    {
        private List<string> ListOfDomains = new List<string>();
        private List<string> ListOfADGroups1 = new List<string>();
        private List<string> ListOfADGroups2 = new List<string>();
        private List<string> ADGroupMembers1 = new List<string>();
        private List<string> ADGroupMembers2 = new List<string>();
        private List<string> MembersToAddToGroup1 = new List<string>();
        private List<string> MembersToRemoveFromGroup1 = new List<string>();
        private List<string> MembersToAddToGroup2 = new List<string>();
        private List<string> MembersToRemoveFromGroup2 = new List<string>();

        public EasyMoveMainForm()
        {
            InitializeComponent();
        }

        private void EasyMoveMainForm_Load(object sender, EventArgs e)
        {
            // check if system is part of a domain
            if (System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain() == null)
            {
                MessageBox.Show("This system is not part of a domain network and cannot manage domain objects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            // check if update is queued
            if (System.IO.File.Exists(Environment.ExpandEnvironmentVariables("%ProgramData%").ToString() + "\\Dobson Utilities\\EasyMove\\Update\\EasyMove.exe"))
            {
                // do nothing
            }

            this.Text = $"LDAP Easy Move { Application.ProductVersion }";
            //this.Text = $"LDAP Easy Move { Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf('.')) }";

            // get list of domains in forest
            using (System.DirectoryServices.ActiveDirectory.Forest forest = System.DirectoryServices.ActiveDirectory.Forest.GetCurrentForest())
            {
                foreach (System.DirectoryServices.ActiveDirectory.Domain domain in forest.Domains)
                {
                    ListOfDomains.Add(domain.Name);
                }
                ListOfDomains.Sort();
            }
            DomainListBox.DataSource = ListOfDomains; // bind to dropdown box
            GetDomainGroups(); // populate group lists
            ADGroupCombo1.DataSource = ListOfADGroups1; // bind to dropdown box
            ADGroupCombo2.DataSource = ListOfADGroups2; // bind to dropdown box
        }

        private void GetDomainGroups()
        {
            ListOfADGroups1.Clear();
            ListOfADGroups2.Clear();

            // populate comboboxes with adgroups
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, DomainListBox.SelectedValue.ToString()))
            using (System.DirectoryServices.AccountManagement.GroupPrincipal groups = new System.DirectoryServices.AccountManagement.GroupPrincipal(context))
            using (System.DirectoryServices.AccountManagement.PrincipalSearcher searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher(groups))
            {
                foreach (System.DirectoryServices.AccountManagement.Principal group in searcher.FindAll())
                {
                    if (group.Name != null)
                    {
                        ListOfADGroups1.Add(group.Name);
                        ListOfADGroups2.Add(group.Name);
                    }
                }
            }
            ListOfADGroups1.Sort();
            ListOfADGroups2.Sort();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (MembersToAddToGroup1.Count == 0 && MembersToAddToGroup2.Count == 0 && MembersToRemoveFromGroup1.Count == 0 && MembersToRemoveFromGroup2.Count == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                DialogResult result = MessageBox.Show("Apply changes and exit?", "Confirm changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ApplyChanges();
                    Application.Exit();
                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (MembersToAddToGroup1.Count == 0 && MembersToAddToGroup2.Count == 0 && MembersToRemoveFromGroup1.Count == 0 && MembersToRemoveFromGroup2.Count == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                DialogResult result = MessageBox.Show("Cancel changes and exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    Application.Exit();
            }
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apply changes?", "Confirm changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                ApplyChanges();
        }

        private void ADGroupCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ADGroupMembers1.Clear(); // clear out existing list
            ADGroupListbox1.Items.Clear(); // clear out list box
            // populate listboxes with ad group member names
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, DomainListBox.SelectedValue.ToString()))
            using (System.DirectoryServices.AccountManagement.GroupPrincipal group = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, ADGroupCombo1.SelectedItem.ToString()))
            {
                foreach (System.DirectoryServices.AccountManagement.Principal principal in group.GetMembers())
                {
                    if (!string.IsNullOrEmpty(principal.UserPrincipalName))
                        ADGroupMembers1.Add(principal.UserPrincipalName);
                }

                ADGroupMembers1.Sort();

                foreach (object user in ADGroupMembers1)
                {
                    if (!string.IsNullOrEmpty(user.ToString()))
                        ADGroupListbox1.Items.Add(user);
                }
            }
            ApplyBtn.Enabled = false;
            Cursor.Current = Cursors.Arrow;
        }

        private void ADGroupCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ADGroupMembers2.Clear(); // clear out existing list
            ADGroupListbox2.Items.Clear(); // clear out list box
            // populate listboxes with ad group member names
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, DomainListBox.SelectedValue.ToString()))
            using (System.DirectoryServices.AccountManagement.GroupPrincipal group = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, ADGroupCombo2.SelectedItem.ToString()))
            {
                foreach (System.DirectoryServices.AccountManagement.Principal principal in group.GetMembers())
                {
                    if (!string.IsNullOrEmpty(principal.UserPrincipalName))
                        ADGroupMembers2.Add(principal.UserPrincipalName);
                }

                ADGroupMembers2.Sort();

                foreach (object user in ADGroupMembers2)
                {
                    if (!string.IsNullOrEmpty(user.ToString()))
                        ADGroupListbox2.Items.Add(user);
                }
            }
            ApplyBtn.Enabled = false;
            Cursor.Current = Cursors.Arrow;
        }

        private void MoveToGroup2Button_Click(object sender, EventArgs e)
        {
            if (ADGroupCombo1.SelectedIndex != ADGroupCombo2.SelectedIndex)
            {
                foreach (int i in ADGroupListbox1.SelectedIndices)
                {
                    ADGroupMembers1.Remove(ADGroupListbox1.Items[i].ToString());
                    if (!MembersToRemoveFromGroup1.Contains(ADGroupListbox1.Items[i].ToString()))
                        MembersToRemoveFromGroup1.Add(ADGroupListbox1.Items[i].ToString());
                    if (!ADGroupMembers2.Contains(ADGroupListbox1.Items[i].ToString()))
                        ADGroupMembers2.Add(ADGroupListbox1.Items[i].ToString());
                    if (!MembersToAddToGroup2.Contains(ADGroupListbox1.Items[i].ToString()))
                        MembersToAddToGroup2.Add(ADGroupListbox1.Items[i].ToString());
                }

                // refresh list displays
                ADGroupListbox1.Items.Clear();
                ADGroupListbox2.Items.Clear();
                ADGroupMembers1.Sort();
                ADGroupMembers2.Sort();
                foreach (object user in ADGroupMembers1)
                    ADGroupListbox1.Items.Add(user);
                foreach (object user in ADGroupMembers2)
                    ADGroupListbox2.Items.Add(user);

                // determine whether to enable apply button
                if (MembersToAddToGroup1.Count == 0 && MembersToAddToGroup2.Count == 0 && MembersToRemoveFromGroup1.Count == 0 && MembersToRemoveFromGroup2.Count == 0)
                    ApplyBtn.Enabled = false;
                else
                    ApplyBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Cannot move to the same group.", "Move error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MoveToGroup1Button_Click(object sender, EventArgs e)
        {
            if (ADGroupCombo1.SelectedIndex != ADGroupCombo2.SelectedIndex)
            {
                foreach (int i in ADGroupListbox2.SelectedIndices)
                {
                    ADGroupMembers2.Remove(ADGroupListbox2.Items[i].ToString());
                    if (!MembersToRemoveFromGroup2.Contains(ADGroupListbox2.Items[i].ToString()))
                        MembersToRemoveFromGroup2.Add(ADGroupListbox2.Items[i].ToString());
                    if (!ADGroupMembers1.Contains(ADGroupListbox2.Items[i].ToString()))
                        ADGroupMembers1.Add(ADGroupListbox2.Items[i].ToString());
                    if (!MembersToAddToGroup1.Contains(ADGroupListbox2.Items[i].ToString()))
                        MembersToAddToGroup1.Add(ADGroupListbox2.Items[i].ToString());
                }

                // refresh list displays
                ADGroupListbox1.Items.Clear();
                ADGroupListbox2.Items.Clear();
                ADGroupMembers1.Sort();
                ADGroupMembers2.Sort();
                foreach (object user in ADGroupMembers1)
                    ADGroupListbox1.Items.Add(user);
                foreach (object user in ADGroupMembers2)
                    ADGroupListbox2.Items.Add(user);

                // determine whether to enable apply button
                if (MembersToAddToGroup1.Count == 0 && MembersToAddToGroup2.Count == 0 && MembersToRemoveFromGroup1.Count == 0 && MembersToRemoveFromGroup2.Count == 0)
                    ApplyBtn.Enabled = false;
                else
                    ApplyBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Cannot move to the same group.", "Move error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyChanges()
        {
            if (MembersToAddToGroup1.Count > 0)
            {
                foreach (string user in MembersToAddToGroup1)
                {
                    AddUserToGroup(user, ADGroupCombo1.SelectedItem.ToString());
                }
                MembersToAddToGroup1.Clear();
            }
            if (MembersToRemoveFromGroup1.Count > 0)
            {
                foreach (string user in MembersToRemoveFromGroup1)
                {
                    RemoveUserFromGroup(user, ADGroupCombo1.SelectedItem.ToString());
                }
                MembersToRemoveFromGroup1.Clear();
            }
            if (MembersToAddToGroup2.Count > 0)
            {
                foreach (string user in MembersToAddToGroup2)
                {
                    AddUserToGroup(user, ADGroupCombo2.SelectedItem.ToString());
                }
                MembersToAddToGroup2.Clear();
            }
            if (MembersToRemoveFromGroup2.Count > 0)
            {
                foreach (string user in MembersToRemoveFromGroup2)
                {
                    RemoveUserFromGroup(user, ADGroupCombo2.SelectedItem.ToString());
                }
                MembersToRemoveFromGroup2.Clear();
            }
            ApplyBtn.Enabled = false;
        }

        private void AddUserToGroup(string userPrincipalName, string groupName)
        {
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, DomainListBox.SelectedValue.ToString()))
            {
                try
                {
                    System.DirectoryServices.AccountManagement.GroupPrincipal group = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, groupName);
                    group.Members.Add(context, System.DirectoryServices.AccountManagement.IdentityType.UserPrincipalName, userPrincipalName);
                    group.Save();
                }
                catch (System.DirectoryServices.DirectoryServicesCOMException e)
                {
                    MessageBox.Show($"Could not add { userPrincipalName } to { groupName }\n\n { e.Message.ToString() }", "Error adding user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.NullReferenceException n)
                {
                    MessageBox.Show($"{ n.Data }\n{ n.Message.ToString() }", "Null Reference exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.UnauthorizedAccessException e)
                {
                    MessageBox.Show(e.Message.ToString(), "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.DirectoryServices.AccountManagement.PrincipalExistsException e)
                {
                    MessageBox.Show(e.Message.ToString(), "Move error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoveUserFromGroup(string userPrincipalName, string groupName)
        {
            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, DomainListBox.SelectedValue.ToString()))
            {
                try
                {
                    System.DirectoryServices.AccountManagement.GroupPrincipal group = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, groupName);
                    group.Members.Remove(context, System.DirectoryServices.AccountManagement.IdentityType.UserPrincipalName, userPrincipalName);
                    group.Save();
                }
                catch (System.DirectoryServices.DirectoryServicesCOMException e)
                {
                    MessageBox.Show($"Could not move { userPrincipalName } to { groupName }\n\n { e.Message.ToString() }", "Error removing user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.NullReferenceException n)
                {
                    MessageBox.Show($"{ n.Data }\n{ n.Message.ToString() }", "Null reference exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.UnauthorizedAccessException e)
                {
                    MessageBox.Show(e.Message.ToString(), "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.DirectoryServices.AccountManagement.PrincipalExistsException e)
                {
                    MessageBox.Show(e.Message.ToString(), "Move error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DomainListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDomainGroups();
        }
    }
}
