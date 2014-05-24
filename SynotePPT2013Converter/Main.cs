using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.IO.Compression;
using System.IO;
using System.Threading;
using System.Diagnostics;
using NAudio;
using NAudio.Wave;
using NAudio.MediaFoundation;

namespace SynotePPT2013Converter
{
    public partial class Main : Form
    {
        private string sourcePPTXPath = "";
        private string intermediatePPTXPath = "";
        private BackgroundWorker worker = new BackgroundWorker();

        public Main()
        {
            InitializeComponent();
            //button_Go_Click(null, null);
        }

        private void button_BrowsePPTXfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PowerPoint 2013 files (.pptx)|*.pptx|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_PPTXPath.Text = openFileDialog.FileName;
            }
        }



        private static void WmaToWav(string wmaFile, string outputFile)
        {
            using (var reader = new MediaFoundationReader(wmaFile))
            {
                WaveFileWriter.CreateWaveFile(outputFile, reader);
                reader.Close();
            }            
        }

        private static void extractPPTXData(string pptxPath)
        {
            List<string> slideNotes = new List<string>();
            List<string> slideTitles = new List<string>();
            List<string> slideTexts = new List<string>();
            List<object> slideNarrations = new List<object>();

            PowerPoint.Application powerpointapplication = new PowerPoint.Application();
            PowerPoint.Presentation powerpointpresentation
                = powerpointapplication.Presentations.Open(pptxPath,
                WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
            int slideCount = powerpointpresentation.Slides.Count;

            foreach (PowerPoint.Slide sld in powerpointpresentation.Slides)
            {
                //Console.WriteLine(sld.Name);

                #region get slide notes
                if (sld.HasNotesPage == Microsoft.Office.Core.MsoTriState.msoTrue)
                {
                    slideNotes.Add(sld.NotesPage.Shapes[2].TextFrame.TextRange.Text);
                }
                #endregion

                #region get slide Title and Text
                string slideText = "";
                string slideTitle = "";
                foreach (PowerPoint.Shape shp in sld.Shapes)
                {
                    if (shp.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                    {
                        if (slideTitle.Equals(string.Empty))
                        {
                            slideTitle = shp.TextFrame.TextRange.Text;
                        }
                        else
                        {
                            slideText += shp.TextFrame.TextRange.Text;
                        }
                    }
                }
                slideTitles.Add(slideTitle);
                slideTexts.Add(slideText);
                #endregion

            }


            powerpointpresentation.Close();
            powerpointapplication.Quit();

            //Console.WriteLine(slideCount);
        }

        private void button_Go_Click(object sender, EventArgs e)
        {
            string testPptxPath2010 = @"D:\Dropbox\ECS\Year4\AssistiveTech\Slides.pptx";

            backgroundWorker_Converter.RunWorkerAsync();
        }
       
        private void backgroundWorker_Converter_DoWork(object sender, DoWorkEventArgs e)
        {
            string testPptxPath2013 = textBox_PPTXPath.Text; //@"D:\Dropbox\ECS\Year4\IRP\pptx13conv\pptx2013presentationTest.pptx";
            string testPptxPath2013zip = Path.GetDirectoryName(testPptxPath2013)
                + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(testPptxPath2013)
                + ".zip";

            backgroundWorker_Converter.ReportProgress(0, "Processing " + testPptxPath2013);
            Thread.Sleep(500);
            //textBox_Log.Text += "\r\nProcessing " + testPptxPath2013;

            // copy and rename pptx file to zip
            File.Copy(testPptxPath2013, testPptxPath2013zip);
            backgroundWorker_Converter.ReportProgress(10, "\r\nCopy and renamed " + testPptxPath2013 + " to " + testPptxPath2013zip);

            // find paths of media*.wma files
            string testPptxPath2013ZipExtracted = Path.GetDirectoryName(testPptxPath2013)
                + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(testPptxPath2013)
                + Path.DirectorySeparatorChar;
            ZipFile.ExtractToDirectory(testPptxPath2013zip, testPptxPath2013ZipExtracted);
            string zipExtractedMediaFolderPath = testPptxPath2013ZipExtracted
                + "ppt"
                + Path.DirectorySeparatorChar
                + "media"
                + Path.DirectorySeparatorChar;
            backgroundWorker_Converter.ReportProgress(20, "\r\nMedia Directory: " + zipExtractedMediaFolderPath);
            Thread.Sleep(500);

            List<string> narrationFies = new List<string>
                (Directory.GetFiles(zipExtractedMediaFolderPath, "media*.wma"));

            // run WmaToWav on the media*.wma files
            foreach (string mediaPath in narrationFies)
            {
                backgroundWorker_Converter.ReportProgress(30, "\r\nConverting to wav: " + mediaPath);
                Thread.Sleep(500);
                string wavPath = Path.GetDirectoryName(mediaPath)
                    + Path.DirectorySeparatorChar
                    + Path.GetFileNameWithoutExtension(mediaPath)
                    + ".wav";
                WmaToWav(mediaPath, wavPath);
            }
            Thread.Sleep(500);
            backgroundWorker_Converter.ReportProgress(40, "\r\nAll media converted");

            // zip up the intermediate folder
            backgroundWorker_Converter.ReportProgress(50, "\r\nDeleting " + testPptxPath2013zip);
            File.Delete(testPptxPath2013zip);
            //DeleteDirectory(testPptxPath2013ZipExtracted);


            string pptxIntermediatePath =
                Path.GetDirectoryName(testPptxPath2013)
                + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(testPptxPath2013)
                + "_INTERMEDIATE.pptx";

            if (File.Exists(pptxIntermediatePath))
            {
                File.Delete(pptxIntermediatePath);
            }

            Thread.Sleep(500);
            backgroundWorker_Converter.ReportProgress(60, "\r\nZipping " + testPptxPath2013ZipExtracted);
            ZipFile.CreateFromDirectory(testPptxPath2013ZipExtracted,
                pptxIntermediatePath);

            Thread.Sleep(500);
            
            Process.Start("cmd.exe", "/c " + @"rmdir /s/q " + testPptxPath2013ZipExtracted);
            //Directory.Delete(testPptxPath2013ZipExtracted, true);

            //MessageBox.Show("Conversion Completed. Please now run the pptx converter on\r\n"
            //    + pptxIntermediatePath);

            Thread.Sleep(500);
            backgroundWorker_Converter.ReportProgress(100, "\r\nCompleted");
        }

        private void backgroundWorker_Converter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            textBox_Log.Text += e.UserState.ToString();
            progressBar.Value = e.ProgressPercentage;

        }

        private void textBox_Log_TextChanged(object sender, EventArgs e)
        {
            // scroll to the end of the text box
            textBox_Log.SelectionStart = textBox_Log.Text.Length;
            textBox_Log.ScrollToCaret();
        }

    }
}
