namespace Animy
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // ����� ���� �Ǵ� ������Ʈ ���� ��θ� ó��
            if (args.Length > 0)
            {
                string projectFilePath = args[0];
                // ���� ��θ� ó���ϰų� Ư�� �Լ� ȣ��
                ProcessProjectFile(projectFilePath);

                // ���ø����̼��� ���� (�� ����)
                ApplicationConfiguration.Initialize();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Form1(projectFilePath));
            }
            else
            {
                Console.WriteLine("���� ��ΰ� �������� �ʾҽ��ϴ�.");
                // ���ø����̼��� ���� (�� ����)
                ApplicationConfiguration.Initialize();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Form1());
            }


        }

        static void ProcessProjectFile(string projectFilePath)
        {
            // ���޵� ������Ʈ ���� ��� ó��
            Console.WriteLine($"������Ʈ ������ ó�� ��: {projectFilePath}");

            // ���÷� ������ �аų� �ٸ� �۾��� ����
            // ��: Ư�� ������Ʈ ������ �ε��ϰ� �����͸� ó���ϴ� ����
            // ���÷� �Ʒ��� ���� Ư�� �Լ� ȣ��
            HandleFileData(projectFilePath);
        }

        static void HandleFileData(string projectFilePath)
        {
            // ������Ʈ ������ ó���ϴ� �Լ�
            Console.WriteLine($"������Ʈ ���� '{projectFilePath}' �����͸� ó�� ��...");
            // ���⼭ ������ ó���ϴ� ������ �߰�

        }

    }
}
