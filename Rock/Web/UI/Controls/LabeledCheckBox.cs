﻿//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rock.Web.UI.Controls
{
    /// <summary>
    /// A <see cref="T:System.Web.UI.WebControls.TextBox"/> control with an associated label.
    /// </summary>
    [ToolboxData( "<{0}:LabeledCheckBox runat=server></{0}:LabeledTextBox>" )]
    public class LabeledCheckBox : CheckBox, ILabeledControl
    {
        /// <summary>
        /// The label
        /// </summary>
        private Literal label;

        /// <summary>
        /// The help block
        /// </summary>
        private HelpBlock helpBlock;

        /// <summary>
        /// Gets or sets the help tip.
        /// </summary>
        /// <value>
        /// The help tip.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "" ),
        Description( "The help tip." )
        ]
        public string Tip
        {
            get
            {
                string s = ViewState["Tip"] as string;
                return s == null ? string.Empty : s;
            }

            set
            {
                ViewState["Tip"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the help block.
        /// </summary>
        /// <value>
        /// The help block.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "" ),
        Description( "The help block." )
        ]
        public string Help
        {
            get
            {
                EnsureChildControls();
                return helpBlock.Text;
            }

            set
            {
                EnsureChildControls();
                helpBlock.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>
        /// The label text.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "" ),
        Description( "The text for the label." )
        ]
        public string LabelText
        {
            get
            {
                EnsureChildControls();
                return label.Text;
            }

            set
            {
                EnsureChildControls();
                label.Text = value;
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Controls.Clear();

            label = new Literal();
            Controls.Add( label );

            helpBlock = new HelpBlock();
            Controls.Add( helpBlock );
        }

        /// <summary>
        /// Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
        public override void RenderControl( HtmlTextWriter writer )
        {
            if ( this.Visible )
            {
                bool renderControlGroupDiv = ( !string.IsNullOrWhiteSpace( LabelText ) || !string.IsNullOrWhiteSpace( Help ) );

                writer.AddAttribute( "class", "checkbox" );
                writer.RenderBeginTag( HtmlTextWriterTag.Div );

                writer.RenderBeginTag( HtmlTextWriterTag.Label );
                label.Visible = this.Visible;
                label.RenderControl( writer );
                if ( !string.IsNullOrWhiteSpace( LabelText ) )
                {
                    helpBlock.RenderControl( writer );
                }

                if (Enabled)
                {
                    base.RenderControl(writer);

                    if (renderControlGroupDiv && string.IsNullOrWhiteSpace(LabelText))
                    {
                        helpBlock.RenderControl(writer);
                    }
                }
                else
                {
                    if (this.Checked)
                    {
                        writer.WriteLine("<i class=\"icon-check\"></i>");
                    }
                    else
                    {
                        writer.WriteLine("<i class=\"icon-check-empty\"></i>");
                    }
                }
                
                writer.RenderEndTag();

                if ( !string.IsNullOrWhiteSpace( Tip ) )
                {
                    writer.AddAttribute( "class", "help-tip" );
                    writer.AddAttribute( "href", "#" );
                    writer.RenderBeginTag( HtmlTextWriterTag.A );
                    writer.RenderBeginTag( HtmlTextWriterTag.Span );
                    writer.Write( Tip.Trim() );
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }

                if ( renderControlGroupDiv )
                {
                    writer.RenderEndTag();
                }
            }
        }
    }
}