﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Manning.MyPhotoAlbum;
using System.Collections.Specialized;


namespace Manning.MyPhotoControls
{
    public partial class PhotoEditDialog : Manning.MyPhotoControls.BaseEditDialog
    {
        private Photograph _photo;
        private Photograph Photo
        {
            get { return _photo; }
        }

        private AlbumManager _manager = null;
        private AlbumManager Manager
        {
            get { return _manager; }
        }


        protected PhotoEditDialog()
        {
            InitializeComponent();
        }

        public PhotoEditDialog(Photograph photo ): this()
        {
            if (photo == null)
                throw new ArgumentException("the photo parameter cannot be null");
            InitializeDialog(photo);
        }
        public PhotoEditDialog(AlbumManager mgr) : this()
        {
            if (mgr == null)
                throw new ArgumentException("The mgr parameter cannot be null");

            _manager = mgr;
            InitializeDialog(mgr.Current);
        }

        private void InitializeDialog(Photograph photo)
        {
            _photo = photo;
            ResetDialog();
          
        }

        protected override void ResetDialog()
        {
            // Fill combo box with photographers in album
            cmbPhotographer.BeginUpdate();
            cmbPhotographer.Items.Clear();

            if (Manager != null)
            {
                StringCollection coll = Manager.Photographers;
                foreach (string s in coll)
                    cmbPhotographer.Items.Add(s);
            }
            else
                cmbPhotographer.Items.Add(Photo.photographer);
            cmbPhotographer.EndUpdate();
            Photograph photo = Photo;
            if(photo != null)
            {
                txtPhotoFile.Text = photo.FileName;
                txtCaption.Text = photo.caption;
                dtpDateTaken.Value = photo.dateaTaken;
                cmbPhotographer.Text = photo.photographer;
                txtNotes.Text = photo.notes;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                SaveSettings();
        }

        private void SaveSettings()
        {
            Photograph photo = Photo;
            if(photo!= null)
            {
                photo.caption = txtCaption.Text;
                photo.photographer = cmbPhotographer.Text;
                photo.notes = txtNotes.Text;
                try
                {
                    photo.dateaTaken = dtpDateTaken.Value;
                }
                catch (FormatException) { }
            }

        }

        private void txtCaption_TextChanged(object sender, EventArgs e)
        {
            Text = txtCaption.Text + " - Properties";
        }

        private static class CurrentDate
        {
            public static DateTime Parse (string input)
            {
                DateTime result = DateTime.Parse(input);
                if (result > DateTime.Now)
                    throw new FormatException("The given date is in the future.");
                return result;
            }
        }

        private void mskDateTaken_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if( !e.IsValidInput)
            {
                DialogResult result = MessageBox.Show("The Date Taken entry is invalid or"
                                                      + "in the future and may be ignored."
                                                      + "Do you wish to correct this?",
                                                      "Photo Properties",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);
                e.Cancel = (result == DialogResult.Yes);
                                                     

            }
        }

        private void cmbPhotographer_Leave(object sender, EventArgs e)
        {
            string person = cmbPhotographer.Text;
            if (!cmbPhotographer.Items.Contains(person))
                cmbPhotographer.Items.Add(person);
        }
    }
}
