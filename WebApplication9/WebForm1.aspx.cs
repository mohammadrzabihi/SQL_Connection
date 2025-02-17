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
                // Validate the inputs
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblErrorMessage.Text = "Please fill in all the fields.";
                    return;
                }

                // Check gender selection
                if (ddlGender.SelectedIndex == 0)
                {
                    lblErrorMessage.Text = "Please select a gender.";
                    return;
                }

                // Get values from the form
                string name = txtName.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                int genderId = Convert.ToInt32(ddlGender.SelectedValue);
                int militaryServiceId = Convert.ToInt32(ddlMilitaryService.SelectedValue);

                // Create the actor object
                Actor.strcActor newActor = new Actor.strcActor
                {
                    Name = name,
                    Username = username,
                    Password = password,
                    IdGenderType = genderId,
                    IdMilitaryServiceType = militaryServiceId
                };

                // Insert the actor into the database
                actorManager.Insert(newActor);

                // Clear the input fields
                ClearInputFields();

                // Refresh the grid view
                BindData();

                // Display a success message
                lblSuccessMessage.Text = "بازیگر با موفقیت اضافه شد!";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                // Display an error message
                lblErrorMessage.Text = "خطا در اضافه کردن بازیگر: " + ex.Message;
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

                // Retrieve values from the GridView's edit row
                string name = ((TextBox)row.FindControl("txtName")).Text;
                string username = ((TextBox)row.FindControl("txtUsername")).Text;
                string password = ((TextBox)row.FindControl("txtPasswordEdit")).Text;

                Actor.strcActor updatedActor = new Actor.strcActor
                {
                    ID = actorID, // Set the ID for the update
                    Name = name,
                    Username = username,  //Get the Username
                    Password = password   // Get the updated password
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
            ddlGender.SelectedIndex = 0; // Reset to the first item
            ddlMilitaryService.SelectedIndex = 0; // Reset to the first item
        }
    }
}
