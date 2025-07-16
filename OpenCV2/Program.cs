using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using OpenCvSharp;

namespace OpenCV2
{
    public enum OpenCvtExam
    {
        Cam,
        Video,
        CvtColor,
        Flip,
        Pyramid,
        Resize,
        Cut,
        Binary,
        BinOperation,
        Bitwise,
        Blur,
        Edge
    }
    public enum ImageFilter
    {
        FilterCanny,
        FilterSobel,
        FilterScharr,
        FilterLaplacian
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            OpenCvtExam exam = OpenCvtExam.Video;

            switch (exam)
            {
                case OpenCvtExam.Cam:
                    ExamCam();
                    break;
                case OpenCvtExam.Video:
                    ExamVideo();
                    break;
                case OpenCvtExam.CvtColor:
                    ExamCvtColor();
                    break;
                case OpenCvtExam.Flip:
                    ExamFlip();
                    break;
                case OpenCvtExam.Pyramid:
                    ExamPyramid();
                    break;
                case OpenCvtExam.Resize:
                    ExamResize();
                    break;
                case OpenCvtExam.Cut:
                    ExamCut();
                    break;
                case OpenCvtExam.Binary:
                    ExamBin();
                    break;
                case OpenCvtExam.BinOperation:
                    ExamBinOperation();
                    break;
                case OpenCvtExam.Bitwise:
                    ExamBitwise();
                    break;
                case OpenCvtExam.Blur:
                    ExamBlurring();
                    break;
                case OpenCvtExam.Edge:
                    ExameEdge();
                    break;
                default:
                    break;
            }

        }
        static public void ExamCam()
        {
            VideoCapture video = new VideoCapture(0); // 카메라를 로딩하는 라이브러리, argument(0): 0번 카메라를 쓴다
            Mat frame = new Mat();
            while (Cv2.WaitKey(33) != 'q')
            {
                video.Read(frame); // 메서드 Read: 카메라 찍는거를 frame에 담는다
                if (!frame.Empty())
                {
                    Cv2.Flip(frame, frame, FlipMode.Y); // ← 여기서 좌우반전
                    Cv2.ImShow("Mirrored Frame", frame);
                }

                //Cv2.ImShow("frame", frame);
            }
            frame.Dispose();
            video.Release();
            Cv2.DestroyAllWindows();
        }
        static public void ExamVideo()
        {

            VideoCapture video = new VideoCapture("C:\\Users\\user\\Desktop\\image\\star.mp4");
            Mat frame = new Mat();
            while (video.PosFrames != video.FrameCount)
            {
                video.Read(frame);
                Cv2.ImShow("frame", frame);
                Cv2.WaitKey(15);
            }
            frame.Dispose();
            video.Release();
            Cv2.DestroyAllWindows();
        }
        static public void ExamCvtColor()
        {
            Mat src = Cv2.ImRead("C:\\Users\\user\\Desktop\\dog.jpg");
            //Mat dst = new Mat(src.Size(), MatType.CV_8UC1);
            Mat dst = new Mat(src.Size(), MatType.CV_8UC1);

            //Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);
            Cv2.ImShow("source", src);
            Cv2.ImShow("destination", dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static public void ExamFlip()
        {
            Mat src = Cv2.ImRead("C:\\Users\\user\\Desktop\\swan.jpg", ImreadModes.ReducedColor4);
            Mat dst = new Mat(src.Size(), MatType.CV_8UC3);
            Mat dst2 = new Mat(src.Size(), MatType.CV_8UC3);

            Cv2.Flip(src, dst, FlipMode.Y);
            Cv2.Flip(src, dst2, FlipMode.XY);
            Cv2.ImShow("source", src);
            Cv2.ImShow("destination", dst);
            Cv2.ImShow("destination2", dst2);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static public void ExamPyramid()
        {
            Mat src = new Mat("C:\\Users\\user\\Desktop\\swan.jpg", ImreadModes.Unchanged);
            Mat pyrUp = new Mat();
            Mat pyrDown = new Mat();

            pyrDown = src.Clone();
            Cv2.PyrUp(src, pyrUp);


            Cv2.ImShow("source", src);
            for (int i = 0; i < 3; i++)
            {
                string windowName = "Pyramid Down" + i;
                Cv2.PyrDown(pyrDown, pyrDown);
                Cv2.ImShow(windowName, pyrDown);
            }

            //Cv2.PyrDown(src, pyrDown);
            //Cv2.ImShow("pyramid up", pyrUp);
            //Cv2.ImShow("pyramid down", pyrDown);
            //Cv2.ImShow("pyramid down2", pyrDown2);
            Cv2.WaitKey(0);

        }
        static public void ExamResize()
        {
            Mat src = new Mat("C:\\Users\\user\\Desktop\\dog.jpg");
            Mat dst = new Mat();
            //Cv2.Resize(src, dst, new Size(300, 400));
            Cv2.Resize(src, dst, new Size(0, 0), 1.5, 1.5);
            Cv2.ImShow("source", src);
            Cv2.ImShow("destination", dst);
            Cv2.WaitKey(0);

        }
        static public void ExamCut()
        {
            Mat src = new Mat("C:\\Users\\user\\Desktop\\glass.jpg");
            Mat dst = src.SubMat(new Rect(300, 300, 500, 300)); // 시작점은 좌 상단

            int srcWidth = src.Size().Width; // 가로
            int srcHeight = src.Size().Height; //세로
            /*
            int devide1 = 2;
            int devide2 = 4;
            int cutWidth = srcWidth / devide2; 
            int cutHeight = srcHeight / devide1;

            Rect rect = new Rect(0, 0, cutWidth, cutHeight);
            for (int i = 0; i <devide2; i++)
            {
                for (int j = 0; j <devide1; j++)
                {
                    int x = i * cutWidth;
                    int y = j * cutHeight;
                    Mat clip = src[y, y + cutHeight, x,x + cutWidth];
                    //Mat clip = src[x, x + cutWidth, y, y + cutHeight];
                    Cv2.ImShow("Clip" + i + j, clip);
                }
            }
            */
            Mat roi1 = new Mat(src, new Rect(300, 300, 100, 100));
            Mat roi2 = src[0, 100, 0, 100];
            Cv2.ImShow("source", src);
            Cv2.ImShow("destination", dst);
            Cv2.ImShow("destination2", roi1);
            Cv2.ImShow("destination3", roi2);
            Cv2.WaitKey(0);
        }
        static public void ExamBin()
        {

            Mat src = new Mat("C:\\Users\\user\\Desktop\\swan.jpg", ImreadModes.ReducedColor2);
            Mat gray = new Mat(src.Size(), MatType.CV_8UC1);
            Mat binary = new Mat(src.Size(), MatType.CV_8UC1);
            Mat otsu = new Mat(src.Size(), MatType.CV_8UC1);
            Mat inRange = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray, binary, 150, 255, ThresholdTypes.Binary);
            Cv2.Threshold(gray, otsu, 0, 255, ThresholdTypes.Otsu);
            Cv2.InRange(gray, 0, 150, inRange);

            Cv2.ImShow("scr", src);
            Cv2.ImShow("gray", gray);
            Cv2.ImShow("binary", binary);
            Cv2.ImShow("otsu", otsu);
            Cv2.ImShow("inrange", inRange);
            Cv2.WaitKey(0);
        }
        static public void ExamBinOperation()
        {

            Mat src = new Mat("C:\\Users\\user\\Desktop\\image\\swan.jpg", ImreadModes.ReducedColor2);
            Mat val = new Mat(src.Size(), MatType.CV_8UC3, new Scalar(1, 8, 1)); //BGR 
            Mat add = new Mat();
            Mat sub = new Mat();
            Mat mul = new Mat();
            Mat div = new Mat();
            Mat max = new Mat();
            Mat min = new Mat();
            Mat abs = new Mat();
            Mat absDiff = new Mat();
            Cv2.Add(src, val, add); //덧셈
            Cv2.Subtract(src, val, sub); //뺄셈
            Cv2.Multiply(src, val, mul); //곱셈
            Cv2.Divide(src, val, div); //나눗셈
            Cv2.Max(src, val, max);
            Cv2.Min(src, val, min);
            Cv2.Abs(sub);
            Cv2.Absdiff(src, mul, absDiff);

            Cv2.ImShow("scr", src);
            Cv2.ImShow("add", add);
            Cv2.ImShow("sub", sub);
            Cv2.ImShow("multi", mul);
            Cv2.ImShow("div", div);
            Cv2.ImShow("max", max);
            Cv2.ImShow("min", min);
            Cv2.ImShow("absDiff", absDiff);

            Cv2.WaitKey(0);

        }

        static public void ExamBitwise()
        {
            Mat src = new Mat("C:\\Users\\user\\Desktop\\image\\fairy.png", ImreadModes.ReducedColor2);
            Mat src2 = src.Flip(FlipMode.Y); //좌우대칭
            Mat and = new Mat();
            Mat or = new Mat();
            Mat xor = new Mat();

            Mat not = new Mat();
            Mat compare = new Mat();
            Cv2.BitwiseAnd(src, src2, and);

            Cv2.BitwiseOr(src, src2, or);
            Cv2.BitwiseXor(src, src2, xor);
            Cv2.BitwiseNot(src, not);

            Cv2.Compare(src, src2, compare, CmpType.LE);
            Cv2.ImShow("src", src);
            Cv2.ImShow("and", and);

            Cv2.ImShow("or", or);
            Cv2.ImShow("xor", xor);
            Cv2.ImShow("not", not);
            Cv2.ImShow("compare", compare);
            Cv2.WaitKey(0);
        }
        static public void ExamBlurring()
        {
            Mat src = new Mat("C:\\Users\\user\\Desktop\\image\\moon.jpg", ImreadModes.ReducedColor2);
            Mat blur = new Mat();
            Mat box_filter = new Mat();
            Mat median_blur = new Mat();
            Mat gaussian_blur = new Mat();
            Mat bilateral_filter = new Mat();

            Cv2.Blur(src, blur, new Size(9, 9), new Point(-1, -1), BorderTypes.Default);//
            Cv2.BoxFilter(src, box_filter, MatType.CV_8UC3, new Size(9, 9), new Point(-1, -1), true, BorderTypes.Default);
            Cv2.MedianBlur(src, median_blur, 9);
            Cv2.GaussianBlur(src, gaussian_blur, new Size(9, 9), 1, 1, BorderTypes.Default);
            Cv2.BilateralFilter(src, bilateral_filter, 9, 3, 3, BorderTypes.Default);
            Cv2.ImShow("src", src);
            Cv2.ImShow("blur", blur);
            Cv2.ImShow("box", box_filter);
            Cv2.ImShow("median", median_blur);
            Cv2.ImShow("gaussian", gaussian_blur);
            Cv2.ImShow("bilateral", bilateral_filter);

            for (int i = 0; i < 5; i++)
            {
                int kernal = 3 * (i + 1);
                Cv2.Blur(src, blur, new Size(kernal, kernal), new Point(-1, -1));
                Cv2.ImShow("Blurred" + kernal, blur);
            }
            Cv2.WaitKey(0);
        }
        static public void ExameEdge()
        {
            Mat src = Cv2.ImRead("C:\\Users\\user\\Desktop\\image\\tomato.jpg", ImreadModes.ReducedColor2);
            Mat blur = new Mat();
            Mat dst = new Mat();
            //Mat blur = src.Clone();
            Cv2.GaussianBlur(src, blur, new Size(3, 3), 1, 0, BorderTypes.Default);

            ImageFilter selType = ImageFilter.FilterCanny;
            switch (selType)
            {
                case ImageFilter.FilterSobel:
                    Cv2.Sobel(blur, dst, MatType.CV_32F, 1, 0, ksize: 3, scale: 1, delta: 0, BorderTypes.Default);
                    dst.ConvertTo(dst, MatType.CV_8UC1);
                    break;
                case ImageFilter.FilterScharr:
                    Cv2.Scharr(blur, dst, MatType.CV_32F, 1, 0, scale: 1, delta: 0, BorderTypes.Default);
                    dst.ConvertTo(dst, MatType.CV_8UC1);
                    break;
                case ImageFilter.FilterLaplacian:
                    Cv2.Laplacian(blur, dst, MatType.CV_32F, ksize: 3, scale: 1, delta: 0, BorderTypes.Default);
                    dst.ConvertTo(dst, MatType.CV_8UC1);
                    break;
                case ImageFilter.FilterCanny:
                    Cv2.Canny(blur, dst, 100, 200, 3, true);
                    break;
            }
            

            string strOperation = selType.ToString();

            if (selType == ImageFilter.FilterCanny)
            {
                Cv2.ImShow(strOperation, dst);
            }
            else
            {
                Mat result = new Mat();
                Cv2.HConcat(src, dst, result);
                Cv2.ImShow(strOperation, result);

            }
            
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }

}
