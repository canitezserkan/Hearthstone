using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoItX3Lib;
using System.Diagnostics;

namespace GoldHunter
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        public Form1()
        {
            InitializeComponent();
        }

        AutoIt CautoIt = new AutoIt();
        AutoItX3 wBas = new AutoItX3();
        AutoItX3 wKaldir = new AutoItX3();

        public void Form1_Load(object sender, EventArgs e)
        {
            moveToPlay.Interval = 1000;
            mouseDownToPlay.Interval = 450;
            mouseUpToPlay.Interval = 350;
            checkSearchingDeviceColor.Interval = 5000;
            moveToConfirm.Interval = 14000;
            mouseDownToConfirm.Interval = 700;
            mouseUpToConfirm.Interval = 550;
            checkTurnButtonColor.Interval = 2500;
            moveToMiddleCard.Interval = 1000;
            mouseDownToMiddleCard.Interval = 700;
            mouseUpToMiddleCard.Interval = 600;
            moveToDropCard.Interval = 700;
            mouseDownToDropCard.Interval = 700;
            mouseUpToDropCard.Interval = 700;
            moveToClickEndTurn.Interval = 1100;
            mouseDownToEndTurn.Interval = 650;
            mouseUpToEndTurn.Interval = 650;
            moveToClickEmptiness.Interval = 500;
            clickEmptinessToClear.Interval = 1200;
            clickEmptinessAgain.Interval = 700;
            moveToFindError.Interval = 1000;
            mouseDownToFindError.Interval = 750;
            mouseUpToFindError.Interval = 380;
            moveToOption.Interval = 800;
            mouseDownToOption.Interval = 1100;
            mouseUpToOption.Interval = 700;
            moveToConcede.Interval = 400;
            mouseDownToConcede.Interval = 450;
            mouseUpToConcede.Interval = 350;
            moveToClearRanked.Interval = 6500;
            clickToClearRanked.Interval = 4000;
            moveToContinue.Interval = 1000;
            clickContinueToHome.Interval = 2500;
            bindEndToStart.Interval = 3000;


            moveToClickDesktop.Interval = 1000;
            clickToGoDesktop.Interval = 2000;
            moveToBattleNet.Interval = 6000;
            mouseDownToBattleNet.Interval = 1000;
            mouseUpToBattleNet.Interval = 1000;
            moveToClientPlay.Interval = 1500;
            mouseDownToClientPlay.Interval = 700;
            mouseUpToClientPlay.Interval = 700;
            moveToMinimizeBattleNet.Interval = 300;
            mouseDownToMinimize.Interval = 600;
            mouseUpToMinimize.Interval = 450;
            MoveToClickOk.Interval = 20000;
            mouseDownToOk.Interval = 700;
            mouseUpToOk.Interval = 700;
            moveToCheckScreen.Interval = 1100;
            clickCheckScreen.Interval = 400;
            moveToMenuPlay.Interval = 3000;
            mouseDownToClientPlay.Interval = 700;
            mouseUpToClientPlay.Interval = 500;
            bindResetToPlay.Interval = 5500;
        }

        public static String RGBConverterYourTurn(System.Drawing.Color c) //YOUR TURN BUTONUNA BAKIYOR
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

        public static String RGBConverterConcede(System.Drawing.Color concede) //CONCEDE TUŞUNUN KIRMIZI OLUP OLMADIĞINI ARIYOR
        {
            return "RGB(" + concede.R.ToString() + "," + concede.G.ToString() + "," + concede.B.ToString() + ")";   
        }

        public static String RGBConverterConcedeCursor(System.Drawing.Color cc) //CONCEDE TUŞUNUN KIRMIZI OLUP OLMADIĞINI ARIYOR
        {
            return "RGB(" + cc.R.ToString() + "," + cc.G.ToString() + "," + cc.B.ToString() + ")";
        }

        public static String RGBConverterConcedeDo(System.Drawing.Color cd) //CONCEDE TUŞUNUN KIRMIZI OLUP OLMADIĞINI ARIYOR
        {
            return "RGB(" + cd.R.ToString() + "," + cd.G.ToString() + "," + cd.B.ToString() + ")";
        }

        public static String RGBConverterGameSearch(System.Drawing.Color tr) //CONCEDE TUŞUNUN KIRMIZI OLUP OLMADIĞINI ARIYOR
        {
            return "RGB(" + tr.R.ToString() + "," + tr.G.ToString() + "," + tr.B.ToString() + ")";
        }

        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        int j = 1009;
        int i = 388;
        int counterFirstDo;
        int counterSecondDo;
        int sonuc;
        int yKordinat = 388;
        int checkColorTimer = 0; 

        public void button1_Click(object sender, EventArgs e) //BAŞLAT BUTON'U
        {
                button1.Enabled = false;
                moveToPlay.Start();
                cbRanked.Enabled = false;
                cbExpKas.Enabled = false;
                checkColorTimer = 0;
                turnCounter = 0;
        }

        public void moveToPlay_Tick(object sender, EventArgs e) //OYNA TUŞUNA GİT
        {
            checkColorTimer = 0;
            if (Process.GetProcessesByName("Hearthstone","RUNNER").Length > 0)
            {
                CautoIt.mMove(1405, 888);
                mouseDownToPlay.Start();
                moveToPlay.Stop();
            }
            else 
            {
                moveToClickDesktop.Start();
                moveToPlay.Stop();             
            }
        }

        private void mouseDownToPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToPlay.Start();
            mouseDownToPlay.Stop();
        }

        private void mouseUpToPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            checkSearchingDeviceColor.Start();
            mouseUpToPlay.Stop();
        }

        private void checkSearchingDeviceColor_Tick(object sender, EventArgs e)
        {
            checkSearchingDeviceColor.Stop();
            Point cursor = new Point(1373, 376); // 90,73,64 renk turnike ve (1373,376) kordinatına bakıyor renk için
            var tr = GetColorAt(cursor);
            if (RGBConverterGameSearch(tr) != "RGB(90,73,64)" && RGBConverterGameSearch(tr) != "RGB(88,73,64)")
            {
                if (cbExpKas.Checked == true)  //BU İF VE HEMEN ALTINDAKİ ELSE, EXP KASARSAK 4 TUR OYNAR, YOKSA DİREK OYUN BAŞLAYINCA CONCEDE İÇİN HAZIRLIĞA GEÇER
                {
                    moveToConfirm.Start();
                }
                else
                {
                    moveToClickEmptiness.Start();
                }

            }
            else 
            {
                checkSearchingDeviceColor.Start();
                //YANİ OYUN ARAMA HALA DEVAM EDİYORSA, RENK AYNI İSE SEARCH DEVİCE KORDİNATINDA, TEKRAR CHECK ETMEK ÜZERE BAŞA SAR BU TIMER'I
            }
            label2.Text = RGBConverterGameSearch(tr);
        }

        private void moveToConfirm_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(965, 856);
            mouseDownToConfirm.Start();
            moveToConfirm.Stop();
        }

        private void mouseDownToConfirm_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToConfirm.Start();
            mouseDownToConfirm.Stop();
        }

        private void mouseUpToConfirm_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            checkTurnButtonColor.Start();
            mouseUpToConfirm.Stop();
        }

        public void checkTurnButtonColor_Tick(object sender, EventArgs e) //END TURN RENGİ TIKLANACAK GİBİYSE TIKLIYORUZ, YOKSA COUNTER KOYARAK BU TİMER'I DÖNGÜYE SOKUYORUZ AMA COUNTER BELLİ SAYIYA GELDİ Mİ OYUNU AÇ KAPA YAPIP COUNTER'I SIFIRLIYORUZ ÇÜNKÜ BÜYÜK İHTİMAL RAKİP CONCEDE VERDİ
        {
            checkTurnButtonColor.Stop();
            Point cursor = new Point(1553, 507);
            var c = GetColorAt(cursor);
            label2.Text = RGBConverterYourTurn(c);
            if (RGBConverterYourTurn(c) == "RGB(207,168,0)" ||
                RGBConverterYourTurn(c) == "RGB(43,174,2)" ||
                RGBConverterYourTurn(c) == "RGB(151,107,0)" ||
                RGBConverterYourTurn(c) == "RGB(31,110,1)" ||
                RGBConverterYourTurn(c) == "RGB(43,163,2)" ||
                RGBConverterYourTurn(c) == "RGB(206,158,0)" ||
                RGBConverterYourTurn(c) == "RGB(28,154,2)" ||
                RGBConverterYourTurn(c) == "RGB(134,149,0)" ||
                RGBConverterYourTurn(c) == "RGB(186,138,0)" ||
                RGBConverterYourTurn(c) == "RGB(39,142,1)" ||
                RGBConverterYourTurn(c) == "RGB(206,168,0)" ||
                RGBConverterYourTurn(c) == "RGB(43,174,2)" ||
                RGBConverterYourTurn(c) == "RGB(43,190,2)" ||
                RGBConverterYourTurn(c) == "RGB(205,184,0)" ||
                RGBConverterYourTurn(c) == "RGB(34,139,1)" ||
                RGBConverterYourTurn(c) == "RGB(163,134,0)" ||
                RGBConverterYourTurn(c) == "RGB(216,178,0)" ||
                RGBConverterYourTurn(c) == "RGB(45,184,2)" ||
                RGBConverterYourTurn(c) == "RGB(199,173,0)" ||
                RGBConverterYourTurn(c) == "RGB(202,165,0)" ||
                RGBConverterYourTurn(c) == "RGB(138,156,0)" ||
                RGBConverterYourTurn(c) == "RGB(166,139,9)")
            {
                label2.Text = RGBConverterYourTurn(c);
                this.BackColor = c;
                moveToMiddleCard.Start();
            }
            else
            {
                checkColorTimer = checkColorTimer + 1;
                if (checkColorTimer != 37)
                {
                    checkTurnButtonColor.Start();
                }
                else
                {
                    Process[] pname = Process.GetProcessesByName("Hearthstone");
                    if (pname.Length == 0)
                    {
                        moveToPlay.Start();
                    }
                    else
                    {
                        Process[] processes = Process.GetProcessesByName("Hearthstone");
                        foreach (var process in processes)
                        {
                            process.Kill();
                        }
                        moveToPlay.Start();
                    }
                }
            }
        }

        private void moveToMiddleCard_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(958, 996);
            mouseDownToMiddleCard.Start();
            moveToMiddleCard.Stop();
            if(turnCounter == 2 || turnCounter == 5) //Turncounter burada arttırılması lazım ki mouseUptoDropCard kısmındaki turncounter ile eşleşmesin. Yoksa tekrar tekrar kart oynar bir tur içinde, eşleştiği takdirde sayılar.
            {
                ++turnCounter;
            }
        }

        private void mouseDownToMiddleCard_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToMiddleCard.Start();
            mouseDownToMiddleCard.Stop();
        }

        private void mouseUpToMiddleCard_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            moveToDropCard.Start();
            mouseUpToMiddleCard.Stop();
        }

        private void moveToDropCard_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1350, 610);
            mouseDownToDropCard.Start();
            moveToDropCard.Stop();
        }

        private void mouseDownToDropCard_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToDropCard.Start();
            mouseDownToDropCard.Stop();
        }

        private void mouseUpToDropCard_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");           
            mouseUpToDropCard.Stop();
            if (turnCounter == 1 || turnCounter == 4) //2 ve 3'üncü turlarda 2 tane kart oynamak için tekrar checkTurnButtonColor'ı başlattık. İlk kartı checkTurnButtonColor kendi kendine oynuyor turnCounter'dan bağımsız. Ek bilgidir.
            {
                ++turnCounter;
                moveToMiddleCard.Start();
            }
            else
            {
                moveToClickEndTurn.Start();
            }
        }

        private void moveToClickEndTurn_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1553, 507);
            mouseDownToEndTurn.Start();
            moveToClickEndTurn.Stop();
        }

        private void mouseDownToEndTurn_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToEndTurn.Start();
            mouseDownToEndTurn.Stop();
        }

        int turnCounter = 0;
        public void mouseUpToEndTurn_Tick(object sender, EventArgs e)
        {
            mouseUpToEndTurn.Stop();
            ++turnCounter;
            checkColorTimer = 0; //END TURN BUTONU'NUN RENGİNİ KONTROL EDEN DİĞER TİMER'IN COUNTER'INI SIFIRLIYORUZ Kİ O VE BURADAKİ TIMER'LAR DÖNGÜYÜ SÜRDÜREBİLSİN YOKSA OYUN KAPANIR ÖBÜR TİMER'IN ELSE SEÇENEĞİNDE 7 KERE CHECK ETTİKTEN SONRA
            CautoIt.mUp("LEFT");
            if(turnCounter >= 7)
            {
                turnCounter = 0; //3 KERE END TURN BUTONUNA BASTIĞIMIZIN GARANTİLİDİR EĞER BU TİMER 4 KERE ÇALIŞMIŞSA VE SIFIRLIYORUZ Kİ SONRAKİ OYUNDA KULLANIMA HAZIR HALE GELSİN BU. ALTTA DA GÖRÜLDÜĞÜ GİBİ CONCEDE İŞLEMİNE BAŞLANIYOR RANKED'TAN İTİBAREN
                moveToClickEmptiness.Start();
            }
            else
            {
                checkTurnButtonColor.Start();
            }
        }


        private void moveToClickEmptiness_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1819, 1001);
            clickEmptinessToClear.Start();
            moveToClickEmptiness.Stop();
        }

        private void clickEmptinessToClear_Tick(object sender, EventArgs e)
        {
            CautoIt.mClick("LEFT", 1819, 1001, 5, 0);
            CautoIt.mClick("LEFT", 1819, 1001, 5, 0);
            clickEmptinessAgain.Start();
            clickEmptinessToClear.Stop();
        }

        private void clickEmptinessAgain_Tick(object sender, EventArgs e)
        {
            CautoIt.mClick("LEFT", 1819, 1001, 5, 0);
            CautoIt.mClick("LEFT", 1819, 1001, 5, 0);
            moveToFindError.Start();
            clickEmptinessAgain.Stop();
        }

        private void moveToFindError_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(949, 646);
            mouseDownToFindError.Start();
            moveToFindError.Stop();
        }

        private void mouseDownToFindError_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToFindError.Start();
            mouseDownToFindError.Stop();
        }

        public void mouseUpToFindError_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            moveToOption.Start();
            mouseUpToFindError.Stop();
        }

        private void moveToOption_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1877, 1054);
            mouseDownToOption.Start();
            moveToOption.Stop();
        }

        private void mouseDownToOption_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToOption.Start();
            mouseDownToOption.Stop();
        }

        public void mouseUpToOption_Tick(object sender, EventArgs e)
        {
            sonuc = 0;
            counterFirstDo = 0;
            int isHsRunning = 1;
            mouseUpToOption.Stop();
            CautoIt.mUp("LEFT");
            Point cCursor = new Point(1009, 388);
            var concede = GetColorAt(cCursor);
            if (RGBConverterConcede(concede) == "RGB(255,255,255)")
            {
                moveToConcede.Start();
                label3.Text = "ilki";
                label4.Text = "NA";
                label5.Text = "NA";
            }
            else
            {
                    do
                    {
                        if(counterFirstDo == 44) //44 kere döngü tekrarlandıysa, concede bayağı bir yukarı kaydı. bu yüzden resetlemek için en alttaki if'de bu 44'ü yakalayıp oyunu kapayacağız.
                        {
                            sonuc = 1;
                        }
                        counterSecondDo = 0;
                        counterFirstDo++;
                        Point ConcedeC = new Point(1009, i - counterFirstDo);
                        var cc = GetColorAt(ConcedeC);
                        yKordinat = i - counterFirstDo;
                        j = 1009;
                        if (RGBConverterConcedeCursor(cc) == "RGB(255,255,255)")
                        {
                            moveToConcede.Start();
                            sonuc = 1;
                            label3.Text = "ikincisi";
                            label4.Text = "NA";
                            label5.Text = "NA";
                        }
                        else
                        {
                            do
                            {
                                counterSecondDo++;
                                j = 1008 - counterSecondDo;
                                Point ConcedeDo = new Point(j, yKordinat); //ilki 994 ve 388
                                var cd = GetColorAt(ConcedeDo); //yeni rengi atadık
                                if (RGBConverterConcedeDo(cd) == "RGB(255,255,255)") 
                                {
                                    moveToConcede.Start();
                                    sonuc = 1;
                                    label4.Text = Convert.ToString(counterFirstDo);
                                    label5.Text = Convert.ToString(counterSecondDo);
                                    label3.Text = "NA";
                                    break;
                                }
                            } while (counterSecondDo != 35); //15 SANIYE BEKLETIR EN FAZLA EĞER CONCEDE TUŞU KAYDIYSA. YOKSA IYI
                        }
                    } while (sonuc != 1);                    
            }
            if (sonuc == 1 && counterFirstDo >= 44)
            {
                Process[] processes = Process.GetProcessesByName("Hearthstone");
                foreach (var process in processes)
                {
                    process.Kill();
                    isHsRunning = 0;
                }
                do
                {
                    Process[] pname = Process.GetProcessesByName("Hearthstone");
                    if (pname.Length == 0)
                    {
                        moveToPlay.Start();
                        isHsRunning = 1;
                    }
                }
                while (isHsRunning == 0);
            }
        }
        

        public void moveToConcede_Tick(object sender, EventArgs e)
        {
            label2.Text = "MOVE";
            CautoIt.mMove(j, yKordinat);
            mouseDownToConcede.Start();
            moveToConcede.Stop();
        }

        private void mouseDownToConcede_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToConcede.Start();
            mouseDownToConcede.Stop();
        }

        private void mouseUpToConcede_Tick(object sender, EventArgs e)
        {
            mouseUpToConcede.Stop();
            CautoIt.mUp("LEFT");
            if (cbRanked.Checked == true)
            { moveToClearRanked.Start(); }
            else
            { moveToContinue.Interval = 6500;
              moveToContinue.Start();}
            
        }

        private void moveToClearRanked_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(980, 500);
            clickToClearRanked.Start();
            moveToClearRanked.Stop();
        }

        private void clickToClearRanked_Tick(object sender, EventArgs e)
        {
            CautoIt.mClick("LEFT", 980, 500, 5, 0);
            CautoIt.mClick("LEFT", 980, 500, 5, 0);
            moveToContinue.Start();
            clickToClearRanked.Stop();
        }

        private void moveToContinue_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1165, 1014);
            clickContinueToHome.Start();
            moveToContinue.Stop();
        }

        private void clickContinueToHome_Tick(object sender, EventArgs e)
        {
            CautoIt.mClick("LEFT", 1165, 1014, 6, 0);
            CautoIt.mClick("LEFT", 1165, 1014, 6, 0);
            bindEndToStart.Start();
            clickContinueToHome.Stop();
        }

        private void bindEndToStart_Tick(object sender, EventArgs e)
        {
            moveToPlay.Start();
            bindEndToStart.Stop();
        }

        private void button2_Click(object sender, EventArgs e) //DURDUR BUTON'U
        {
            moveToPlay.Stop();
            mouseDownToPlay.Stop();
            mouseUpToPlay.Stop();
            checkSearchingDeviceColor.Stop();
            moveToConfirm.Stop();
            mouseDownToConfirm.Stop();
            mouseUpToConfirm.Stop();
            checkTurnButtonColor.Stop();
            moveToMiddleCard.Stop();
            mouseDownToMiddleCard.Stop();
            mouseUpToMiddleCard.Stop();
            moveToDropCard.Stop();
            mouseDownToDropCard.Stop();
            mouseUpToDropCard.Stop();
            moveToClickEndTurn.Stop();
            mouseDownToEndTurn.Stop();
            mouseUpToEndTurn.Stop();
            moveToClickEmptiness.Stop();
            clickEmptinessToClear.Stop();
            clickEmptinessAgain.Stop();
            moveToFindError.Stop();
            mouseDownToFindError.Stop();
            mouseUpToFindError.Stop();
            moveToOption.Stop();
            mouseDownToOption.Stop();
            mouseUpToOption.Stop();
            moveToConcede.Stop();
            mouseDownToConcede.Stop();
            mouseUpToConcede.Stop();
            moveToClearRanked.Stop();
            clickToClearRanked.Stop();
            moveToContinue.Stop();
            clickContinueToHome.Stop();
            bindEndToStart.Stop();

            moveToClickDesktop.Stop();
            clickToGoDesktop.Stop();
            moveToBattleNet.Stop();
            mouseDownToBattleNet.Stop();
            mouseUpToBattleNet.Stop();
            moveToClientPlay.Stop();
            mouseDownToClientPlay.Stop();
            mouseUpToClientPlay.Stop();
            moveToMinimizeBattleNet.Stop();
            mouseDownToMinimize.Stop();
            mouseUpToMinimize.Stop();
            moveToCheckScreen.Stop();
            clickCheckScreen.Stop();
            MoveToClickOk.Stop();
            mouseDownToOk.Stop();
            mouseUpToOk.Stop();
            moveToMenuPlay.Stop();
            mouseDownToClientPlay.Stop();
            mouseUpToClientPlay.Stop();
            bindResetToPlay.Stop();

            button1.Enabled = true;
            cbRanked.Enabled = true;
            cbExpKas.Enabled = true;
            moveToContinue.Interval = 1000;
            yKordinat = 388;
        }

        private void moveToClickDesktop_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1168, 1018);
            clickToGoDesktop.Start();
            moveToClickDesktop.Stop();  
        }

        private void clickToGoDesktop_Tick(object sender, EventArgs e)
        {
            CautoIt.mClick("LEFT", 1168, 1018, 6, 0);
            CautoIt.mClick("LEFT", 1168, 1018, 6, 0);
            moveToBattleNet.Start();
            clickToGoDesktop.Stop();
        }

        private void moveToBattleNet_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(388, 1054);
            mouseDownToBattleNet.Start();
            moveToBattleNet.Stop();
        }

        private void mouseDownToBattleNet_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToBattleNet.Start();
            mouseDownToBattleNet.Stop();
        }

        private void mouseUpToBattleNet_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            moveToClientPlay.Start();
            mouseUpToBattleNet.Stop();
        }

        private void moveToClientPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(745, 833);
            mouseDownToClientPlay.Start();
            moveToClientPlay.Stop(); 
        }

        private void mouseDownToClientPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToClientPlay.Start();
            mouseDownToClientPlay.Stop();
        }

        private void mouseUpToClientPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            moveToMinimizeBattleNet.Start();
            mouseUpToClientPlay.Stop();
        }

        private void moveToMinimizeBattleNet_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(1500, 138);
            mouseDownToMinimize.Start();
            moveToMinimizeBattleNet.Stop();
        }

        private void mouseDownToMinimize_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToMinimize.Start();
            mouseDownToMinimize.Stop();
        }

        private void mouseUpToMinimize_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            MoveToClickOk.Start();
            mouseUpToMinimize.Stop();
        }

        private void MoveToClickOk_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(961, 640);
            mouseDownToOk.Start();
            MoveToClickOk.Stop();
        }

        private void mouseDownToOk_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToOk.Start();
            mouseDownToOk.Stop();
        }

        private void mouseUpToOk_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            moveToCheckScreen.Start();
            mouseUpToOk.Stop();
        }

        private void moveToCheckScreen_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(855, 614);
            clickCheckScreen.Start();
            moveToCheckScreen.Stop();
        }

        private void clickCheckScreen_Tick(object sender, EventArgs e)
        {
            CautoIt.mClick("LEFT", 855, 614, 6, 0);
            CautoIt.mClick("LEFT", 855, 614, 6, 0);
            moveToMenuPlay.Start();
            clickCheckScreen.Stop();
        }

        private void moveToMenuPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mMove(960, 327);
            mouseDownToMenuPlay.Start();
            moveToMenuPlay.Stop();
        }

        private void mouseDownToMenuPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mDown("LEFT");
            mouseUpToMenuPlay.Start();
            mouseDownToMenuPlay.Stop();
        }

        private void mouseUpToMenuPlay_Tick(object sender, EventArgs e)
        {
            CautoIt.mUp("LEFT");
            bindResetToPlay.Start();
            mouseUpToMenuPlay.Stop();
        }

        public void bindResetToPlay_Tick(object sender, EventArgs e)
        {
            moveToPlay.Start();
            bindResetToPlay.Stop();

        }

    }
}


