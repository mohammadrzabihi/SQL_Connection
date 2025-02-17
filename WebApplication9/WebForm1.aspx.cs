using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using WebApplication9.Code;

namespace WebApplication9
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Actor actorManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            actorManager = new Actor();  // Use the constructor that reads from Web.config

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            try
            {
                DataTable dt = actorManager.SelectAll();
                gvActors.DataSource = dt;
                gvActors.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error loading data: " + ex.Message;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Actor.strcActor newActor = new Actor.strcActor
                {
                    Name = txtName.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text
                };

                actorManager.Insert(newActor);
                ClearInputFields();
                BindData();
                lblSuccessMessage.Text = "Actor added successfully!";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error adding actor: " + ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        protected void gvActors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvActors.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvActors_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvActors.EditIndex = -1;
            BindData();
        }

        protected void gvActors_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvActors.Rows[e.RowIndex];
                int actorID = Convert.ToInt32(gvActors.DataKeys[e.RowIndex].Value); // Get the actor ID

                Actor.strcActor updatedActor = new Actor.strcActor
                {
                    ID = actorID, // Set the ID for the update
                    Name = ((TextBox)row.FindControl("txtName")).Text,
                    Username = ((TextBox)row.FindControl("txtUsername")).Text,  //Get the Username
                    Password = ((TextBox)row.FindControl("txtPasswordEdit")).Text  // Get the updated password
                };

                actorManager.Update(updatedActor);
                gvActors.EditIndex = -1;
                BindData();
                lblSuccessMessage.Text = "Actor updated successfully!";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error updating actor: " + ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        protected void gvActors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int actorID = Convert.ToInt32(gvActors.DataKeys[e.RowIndex].Value); // Get the actor ID

                actorManager.Delete(actorID);
                BindData();
                lblSuccessMessage.Text = "Actor deleted successfully!";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error deleting actor: " + ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        private void ClearInputFields()
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}