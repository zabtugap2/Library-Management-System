using LibraryManagementSystem.Database;
using LibraryManagementSystem.Model;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LibraryManagementSystem.UserInterface_Forms
{
    public partial class frmBookControler : Form
    {
        private Book _currentBook;

        public int BookID => _currentBook?.BookID ?? 0;

        public frmBookControler()
        {
            InitializeComponent();
        }

        public void SetData(Book book)
        {
            if (book == null) return;

            _currentBook = book;

            lblTitle.Text = book.Title;
            lblSubTitle.Text = string.IsNullOrEmpty(book.SubTitle) ? "" : book.SubTitle;
            lblAuthor.Text = $"Author: {book.Author}";
            lblPages.Text = $"Pages: {book.Pages}";
            lblISBN.Text = $"ISBN: {book.ISBN}";
            lblLocation.Text = $"Location: {book.Location}";
            lblEditor.Text = $"Editor: {book.Editor}";
            lblAccessionNumber.Text = $"Accession #: {book.AccessionNumber}";
            lblPhysicalDescripton.Text = book.PhysicalDescription ?? "";
            lblPublisher.Text = book.Publisher;
            lblPublicationYear.Text = book.PublicationYear.ToString();
            lblEdition.Text = book.Edition;
            lblLanguage.Text = book.Language;
            lblCategory.Text = book.Category ?? "";

            LoadBookImage(book.BookImagePath);
        }

        private void LoadBookImage(string imagePath)
        {
            try
            {
                pctBook.Image?.Dispose();
                pctBook.Image = null;

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pctBook.Image = Image.FromStream(fs);
                    }

                }
                else
                {
                     pctBook.Image = Properties.Resources.book; // default image
                }
            }
            catch
            {
                  pctBook.Image = Properties.Resources.book;
            }
        }


        private void btnBorrow_Click_1(object sender, EventArgs e)
        {
            if (BookID == 0)
            {
                MessageBox.Show("Invalid book.");
                return;
            }

            try
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();

                    // =========================
                    // 1️⃣ CHECK DOUBLE BORROW
                    // =========================
                    string checkBorrowSql = @"
SELECT COUNT(*) 
FROM BookBorrow
WHERE BookID = @BookID
AND UserID = @UserID
AND Status = 'Borrowed'";

                    SqlCommand checkBorrowCmd = new SqlCommand(checkBorrowSql, con);
                    checkBorrowCmd.Parameters.AddWithValue("@BookID", BookID);
                    checkBorrowCmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);

                    int alreadyBorrowed = (int)checkBorrowCmd.ExecuteScalar();

                    if (alreadyBorrowed > 0)
                    {
                        MessageBox.Show("You already borrowed this book.");
                        return;
                    }

                    // =========================
                    // 2️⃣ CHECK AVAILABILITY
                    // =========================
                    string checkAvailabilitySql = @"
SELECT AvailableCopies 
FROM Books
WHERE BookID = @BookID";

                    SqlCommand checkAvailabilityCmd = new SqlCommand(checkAvailabilitySql, con);
                    checkAvailabilityCmd.Parameters.AddWithValue("@BookID", BookID);

                    int availableCopies = Convert.ToInt32(checkAvailabilityCmd.ExecuteScalar());

                    if (availableCopies <= 0)
                    {
                        MessageBox.Show("This book is currently unavailable.");
                        return;
                    }

                    // =========================
                    // 3️⃣ TRANSACTION
                    // =========================
                    SqlTransaction tran = con.BeginTransaction();

                    try
                    {
                        // INSERT BORROW
                        string borrowSql = @"
INSERT INTO BookBorrow
(BookID, UserID, BorrowDate, DueDate, Status)
VALUES
(@BookID, @UserID, GETDATE(), DATEADD(day, 7, GETDATE()), 'Borrowed')";

                        SqlCommand borrowCmd = new SqlCommand(borrowSql, con, tran); // ✅ Include transaction
                        borrowCmd.Parameters.AddWithValue("@BookID", BookID);
                        borrowCmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);
                        borrowCmd.ExecuteNonQuery();

                        // DECREASE AVAILABLE COPIES
                        string updateBookSql = @"
UPDATE Books
SET AvailableCopies = AvailableCopies - 1
WHERE BookID = @BookID";

                        SqlCommand updateBookCmd = new SqlCommand(updateBookSql, con, tran); // ✅ Include transaction
                        updateBookCmd.Parameters.AddWithValue("@BookID", BookID);
                        updateBookCmd.ExecuteNonQuery();

                        // COMMIT TRANSACTION
                        tran.Commit();

                        MessageBox.Show("Book borrowed successfully!");
                    }
                    catch (Exception ex)
                    {
                        // ROLLBACK if any error occurs
                        tran.Rollback();
                        MessageBox.Show("Borrow failed: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Borrow failed: " + ex.Message);
            }
        }










        private void btnReserve_Click_1(object sender, EventArgs e)
        {
            if (BookID == 0)
            {
                MessageBox.Show("Invalid book.");
                return;
            }

            try
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();

                    // 1️⃣ CHECK IF USER ALREADY BORROWED
                    string checkBorrowSql = @"
SELECT COUNT(*) 
FROM BookBorrow
WHERE BookID = @BookID
AND UserID = @UserID
AND Status = 'Borrowed'";

                    SqlCommand checkBorrowCmd = new SqlCommand(checkBorrowSql, con);
                    checkBorrowCmd.Parameters.AddWithValue("@BookID", BookID);
                    checkBorrowCmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);

                    if ((int)checkBorrowCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("You already borrowed this book.");
                        return;
                    }

                    // 2️⃣ CHECK IF ALREADY RESERVED
                    string checkReserveSql = @"
SELECT COUNT(*)
FROM BookReservation
WHERE BookID = @BookID
AND UserID = @UserID
AND Status = 'Pending'";

                    SqlCommand checkReserveCmd = new SqlCommand(checkReserveSql, con);
                    checkReserveCmd.Parameters.AddWithValue("@BookID", BookID);
                    checkReserveCmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);

                    if ((int)checkReserveCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("You already reserved this book.");
                        return;
                    }

                    // 3️⃣ CHECK AVAILABILITY
                    string availabilitySql = @"
SELECT AvailableCopies 
FROM Books
WHERE BookID = @BookID";

                    SqlCommand availabilityCmd = new SqlCommand(availabilitySql, con);
                    availabilityCmd.Parameters.AddWithValue("@BookID", BookID);

                    int availableCopies = Convert.ToInt32(availabilityCmd.ExecuteScalar());

                    if (availableCopies > 0)
                    {
                        MessageBox.Show("This book is available. Please borrow it instead.");
                        return;
                    }

                    // 4️⃣ INSERT RESERVATION
                    string insertSql = @"
INSERT INTO BookReservation
(BookID, UserID, ReservationDate, Status)
VALUES
(@BookID, @UserID, GETDATE(), 'Pending')";

                    SqlCommand insertCmd = new SqlCommand(insertSql, con);
                    insertCmd.Parameters.AddWithValue("@BookID", BookID);
                    insertCmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Book reserved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reservation failed: " + ex.Message);
            }
        }



        private void pctBook_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lblTitle.Text, "Book Details");
        }
        private void pnlBookDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        

        
    }
}
