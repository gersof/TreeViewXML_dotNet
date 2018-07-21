using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Text;
using System.IO;

namespace TreeViewXML
{
	/// <summary>
	/// Contains methods for loading and saving XML files into/from a Treeview control
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Constructor/Destructor

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(744, 525);
			this.treeView1.TabIndex = 0;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem4,
																					  this.menuItem3});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Open";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem6,
																					  this.menuItem7});
			this.menuItem4.Text = "&Save";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "Use Streamwriter";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "Use XmlTextWriter";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "E&xit";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(744, 525);
			this.Controls.Add(this.treeView1);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Treeview Example";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#region Menu Item Click Events

		//Open XML
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			populateTreeview();
		}

		//Save with StreamWriter
		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			serializeTreeview();
		}

		//Save with XmlTextWriter
		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			serializeTreeview2();
		}

		//Exit the application
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		#endregion

		#region Treeview Population

		//Open the XML file, and start to populate the treeview
		private void populateTreeview()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Open XML Document";
			dlg.Filter = "XML Files (*.xml)|*.xml";
			dlg.FileName = Application.StartupPath + "\\..\\..\\example.xml";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					//Just a good practice -- change the cursor to a wait cursor while the nodes populate
					this.Cursor = Cursors.WaitCursor;

					//First, we'll load the Xml document
					XmlDocument xDoc = new XmlDocument();
					xDoc.Load(dlg.FileName);
			
					//Now, clear out the treeview, and add the first (root) node
					treeView1.Nodes.Clear();
					treeView1.Nodes.Add(new TreeNode(xDoc.DocumentElement.Name));
					TreeNode tNode = new TreeNode();
					tNode = (TreeNode)treeView1.Nodes[0];

					//We make a call to AddNode, where we'll add all of our nodes
					addTreeNode(xDoc.DocumentElement, tNode);

					//Expand the treeview to show all nodes
					treeView1.ExpandAll();
			
				}
				catch(XmlException xExc) //Exception is thrown is there is an error in the Xml
				{
					MessageBox.Show(xExc.Message);
				}
				catch(Exception ex) //General exception
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					this.Cursor = Cursors.Default; //Change the cursor back
				}
			}
		}

		//This function is called recursively until all nodes are loaded
		private void addTreeNode(XmlNode xmlNode, TreeNode treeNode)
		{
			XmlNode xNode;
			TreeNode tNode;
			XmlNodeList xNodeList;

			if (xmlNode.HasChildNodes) //The current node has children
			{
				xNodeList = xmlNode.ChildNodes;

				for(int x=0; x<=xNodeList.Count-1; x++) //Loop through the child nodes
				{
					xNode = xmlNode.ChildNodes[x];
					treeNode.Nodes.Add(new TreeNode(xNode.Name));
					tNode = treeNode.Nodes[x];
					addTreeNode(xNode, tNode);
				}
			}
			else //No children, so add the outer xml (trimming off whitespace)
				treeNode.Text = xmlNode.OuterXml.Trim();
		}

		#endregion

		#region XML Writer Methods

		private void serializeTreeview2()
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.FileName = this.treeView1.Nodes[0].Text + ".xml";
			dlg.Filter = "XML Files (*.xml)|*.xml";			
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				exportToXml2(treeView1, dlg.FileName);
			}
		}	

		//We use this in the exportToXml2 and the saveNode2 functions, though it's only instantiated once.
		private XmlTextWriter xr;

		public void exportToXml2(TreeView tv, string filename) 
		{
			xr = new XmlTextWriter(filename, System.Text.Encoding.UTF8);

			xr.WriteStartDocument();
			//Write our root node
			xr.WriteStartElement(treeView1.Nodes[0].Text);
			foreach (TreeNode node in tv.Nodes)
			{
				saveNode2(node.Nodes);
			}
			//Close the root node
			xr.WriteEndElement();
			xr.Close();
		}

		private void saveNode2(TreeNodeCollection tnc)
		{
			foreach (TreeNode node in tnc)
			{
				//If we have child nodes, we'll write a parent node, then iterrate through
				//the children
				if (node.Nodes.Count > 0)
				{
					xr.WriteStartElement(node.Text);
					saveNode2(node.Nodes);
					xr.WriteEndElement();	
				} 
				else //No child nodes, so we just write the text
				{
					xr.WriteString(node.Text);
				}
			}
		}

		#endregion

		#region Stream Writer Methods

		//Opens a save file dialog so we know where to save the XML. This method uses a streamwriter.
		private void serializeTreeview()
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.FileName = this.treeView1.Nodes[0].Text + ".xml";
			dlg.Filter = "XML Files (*.xml)|*.xml";			
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				exportToXml(treeView1, dlg.FileName);
			}
		}

		//We use this in the export and the saveNode functions, though it's only instantiated once.
		private StreamWriter sr;

		public void exportToXml(TreeView tv, string filename) 
		{
			sr = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
			//Write the header
			sr.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
			//Write our root node
			sr.WriteLine("<" + treeView1.Nodes[0].Text + ">");
			foreach (TreeNode node in tv.Nodes)
			{
				saveNode(node.Nodes);
			}
			//Close the root node
			sr.WriteLine("</" + treeView1.Nodes[0].Text + ">");
			sr.Close();
		}

		private void saveNode(TreeNodeCollection tnc)
		{
			foreach (TreeNode node in tnc)
			{
				//If we have child nodes, we'll write a parent node, then iterrate through
				//the children
				if (node.Nodes.Count > 0)
				{
					sr.WriteLine("<" + node.Text + ">");
					saveNode(node.Nodes);
					sr.WriteLine("</" + node.Text + ">");
				} 
				else //No child nodes, so we just write the text
				{
					sr.WriteLine(node.Text);
				}
			}
		}

		#endregion

	}
}
