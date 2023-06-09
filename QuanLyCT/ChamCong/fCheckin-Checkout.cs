﻿using QLCongTy.ChamCong;
using QLCongTy.QLDuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCongTy
{
    public partial class fCheckin_Checkout : Form
    {
        CheckInOutDAO ciod = new CheckInOutDAO();
        ChamCongDAO ccd = new ChamCongDAO();
        CheckInOut cio = new CheckInOut();
        DuAnDAO dad = new DuAnDAO();
        PCNhanLuc pc = null;
        public fCheckin_Checkout()
        {
            InitializeComponent();
        }

        private void fCheckin_Checkout_Load(object sender, EventArgs e)
        {
            ReLoad();
            txtManvsang.Texts = fMainMenu.MaNV;
            txtManvchieu.Texts = fMainMenu.MaNV;
            txtMacvsang.Texts = fMainMenu.MaCV;
            txtMacvchieu.Texts = fMainMenu.MaCV;
        }

        public void ReLoad()
        {
            gvChecksang.DataSource = ciod.LayDanhSach(fMainMenu.MaNV);
            gvCheckchieu.DataSource = ciod.LayDanhSach(fMainMenu.MaNV);
        }

        public void ConvertCheck(CheckInOut cio)
        {
            if (cbCheckInsang.Checked == true)
            {
                cio.CheckInSang = 1;
            }
            if (cbCheckOutchieu.Checked == true)
            {
                cio.CheckOutChieu = 1;
            }
        }

        public void CheckNgayNghi(CheckInOut cio)
        {
            //Kiểm tra nhân viên đã check đủ 2 buổi không
            bool check = ciod.CheckDiLam(txtManvchieu.Texts, dtpCheckOut.Value);
            if (!check)
            {
                ciod.UpdateNgDiLam(cio.MaNV, cio.Ngay, 0);
            }
            else
            {
                ciod.UpdateNgDiLam(cio.MaNV, cio.Ngay, 1);
            }
        } 

        private void gvCheckchieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = gvCheckchieu.SelectedRows[0];
            pc = new PCNhanLuc();
            pc.Manv = rows.Cells["MaNV"].Value.ToString();
            pc.Mada = rows.Cells["MaDA"].Value.ToString();
            pc.Ngaybd = DateTime.Parse(rows.Cells["NgayBD"].Value.ToString());
            pc.Ngaykt = DateTime.Parse(rows.Cells["NgayKT"].Value.ToString());
        }

        private void btnCapNhatTienDo_Click(object sender, EventArgs e)
        {
            //Điền thông tin điểm danh cho cio
            cio.MaNV = txtManvchieu.Texts;
            cio.Ngay = dtpCheckOut.Value.Date;
            //Cập nhật CheckInOut
            if (pc != null)
            {
                if (int.Parse(txtPhanTram.Texts) >= 0 && int.Parse(txtPhanTram.Texts) <= 100)
                {
                    ciod.DanhGiaCV(int.Parse(txtPhanTram.Texts), pc.Manv, pc.Ngaybd, pc.Ngaykt);
                    dad.TienDoDuAn(pc.Mada);
                }
                else
                {
                    MessageBox.Show("Phần trăm khong hợp lệ");
                }
            }
            ReLoad();
        }

        private void btnSubmitChieu_Click(object sender, EventArgs e)
        {
            ccd.InsertChamCong();
            ConvertCheck(cio);
            ciod.SubmitChieu(cio);
            //Tính số ngày đi làm sau khi checkin_out
            CheckNgayNghi(cio);
            //Thông báo
            MessageBox.Show("Đã check out thành công");
        }

        private void btnSubmitSang_Click(object sender, EventArgs e)
        {
            ConvertCheck(cio);
            cio.MaNV = txtManvsang.Texts;
            cio.Macv = txtMacvsang.Texts;
            cio.Ngay = dtpCheckIn.Value.Date;
            ciod.SubmitSang(cio);
            //Thông báo
            MessageBox.Show("Đã check in thành công");
            ReLoad();
        }
    }
}
