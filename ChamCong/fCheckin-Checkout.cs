﻿using QLCongTy.ChamCong;
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
        CheckInOut cio = new CheckInOut();
        ChamCongDAO ccd = new ChamCongDAO();

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
            gvChecksang.DataSource = ciod.LayDanhSach($"SELECT * FROM PHANCONGDUAN WHERE MaNV = '{fMainMenu.MaNV}'");
            gvCheckchieu.DataSource = ciod.LayDanhSach($"SELECT * FROM PHANCONGDUAN WHERE MaNV = '{fMainMenu.MaNV}'");
        }

        private void btnSubmitsang_Click(object sender, EventArgs e)
        {
            ConvertCheck(cio);
            cio.MaNV = txtManvsang.Texts;
            cio.Macv = txtMacvsang.Texts;
            cio.Ngay = dtpCheckIn.Value.Date;
            ciod.SubmitSang(cio);
            ReLoad();
        }

        private void btnSubmitchieu_Click(object sender, EventArgs e)
        {
            //Điền thông tin điểm danh cho cio
            cio.MaNV = txtManvchieu.Texts;
            cio.Ngay = dtpCheckOut.Value.Date;
            
            ciod.DanhGiaCV(txtPhanTram.Texts, txtMaDa.Texts, txtManvchieu.Texts);
            ConvertCheck(cio);
            ciod.SubmitChieu(cio);
            ReLoad();

            //Kiểm tra tháng, năm chấm công có tồn tại hay chưa
            if (!ccd.InsertChamCong())
            {
                MessageBox.Show("Đã tồn tại dữ liệu chấm công tại thời gian này");
            }
            //Tính số ngày đi làm sau khi checkin_out
            CheckNgayNghi(cio);
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
            txtMaDa.Texts = rows.Cells["MaDA"].Value.ToString();
        }

        private void gvChecksang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow rows = gvChecksang.SelectedRows[0];
            txtMaDa.Texts = rows.Cells["MaDA"].Value.ToString();
        }
    }
}
