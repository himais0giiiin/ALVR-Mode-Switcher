using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: IconGenerator <source-png> <output-ico>");
    return 1;
}

string sourcePath = Path.GetFullPath(args[0]);
string outputPath = Path.GetFullPath(args[1]);

if (!File.Exists(sourcePath))
{
    Console.Error.WriteLine($"Source PNG not found: {sourcePath}");
    return 1;
}

using var source = new Bitmap(sourcePath);
if (source.Width != source.Height)
{
    Console.Error.WriteLine("Source PNG must be square.");
    return 1;
}

int[] sizes = [16, 20, 24, 32, 40, 48, 64, 128, 256];
var images = new List<(int Size, byte[] Data)>();

foreach (int size in sizes)
{
    using Bitmap bitmap = Resize(source, size);
    using var stream = new MemoryStream();
    bitmap.Save(stream, ImageFormat.Png);
    images.Add((size, stream.ToArray()));
}

WriteIcon(outputPath, images);
Console.WriteLine($"Generated {outputPath} from {sourcePath}");
return 0;

static Bitmap Resize(Image source, int size)
{
    var bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
    using Graphics graphics = Graphics.FromImage(bitmap);
    graphics.Clear(Color.Transparent);
    graphics.CompositingMode = CompositingMode.SourceCopy;
    graphics.CompositingQuality = CompositingQuality.HighQuality;
    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
    graphics.SmoothingMode = SmoothingMode.HighQuality;
    graphics.DrawImage(source, new Rectangle(0, 0, size, size));
    return bitmap;
}

static void WriteIcon(string path, IReadOnlyList<(int Size, byte[] Data)> images)
{
    using FileStream stream = File.Create(path);
    using var writer = new BinaryWriter(stream);

    writer.Write((ushort)0);
    writer.Write((ushort)1);
    writer.Write((ushort)images.Count);

    uint offset = (uint)(6 + images.Count * 16);
    foreach ((int size, byte[] data) in images)
    {
        writer.Write((byte)(size >= 256 ? 0 : size));
        writer.Write((byte)(size >= 256 ? 0 : size));
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((ushort)1);
        writer.Write((ushort)32);
        writer.Write((uint)data.Length);
        writer.Write(offset);
        offset += (uint)data.Length;
    }

    foreach ((_, byte[] data) in images)
    {
        writer.Write(data);
    }
}
