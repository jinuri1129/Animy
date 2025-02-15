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

            // 명령줄 인자 또는 프로젝트 파일 경로를 처리
            if (args.Length > 0)
            {
                string projectFilePath = args[0];
                // 파일 경로를 처리하거나 특정 함수 호출
                ProcessProjectFile(projectFilePath);

                // 애플리케이션을 시작 (폼 실행)
                ApplicationConfiguration.Initialize();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Form1(projectFilePath));
            }
            else
            {
                Console.WriteLine("파일 경로가 제공되지 않았습니다.");
                // 애플리케이션을 시작 (폼 실행)
                ApplicationConfiguration.Initialize();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Form1());
            }


        }

        static void ProcessProjectFile(string projectFilePath)
        {
            // 전달된 프로젝트 파일 경로 처리
            Console.WriteLine($"프로젝트 파일을 처리 중: {projectFilePath}");

            // 예시로 파일을 읽거나 다른 작업을 수행
            // 예: 특정 프로젝트 파일을 로드하고 데이터를 처리하는 로직
            // 예시로 아래와 같이 특정 함수 호출
            HandleFileData(projectFilePath);
        }

        static void HandleFileData(string projectFilePath)
        {
            // 프로젝트 파일을 처리하는 함수
            Console.WriteLine($"프로젝트 파일 '{projectFilePath}' 데이터를 처리 중...");
            // 여기서 파일을 처리하는 로직을 추가

        }

    }
}
