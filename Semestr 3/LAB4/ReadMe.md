Видео с работой программой - https://drive.google.com/file/d/1o8rGM0C5Aj_n3M3SLWp43cF5ll5cZeEj/view?usp=sharing
1. (LAB3)FileWatcher
Создан в рамках третьей лабораторной с изменным фильтром реагирования с txt на xml.Находится в другом репозитории https://github.com/Skava600/labs-c-sharp/tree/master/Semestr%203/LAB3
2. DataAccessLayer : IDataAccessLayer
Позволяет вытаскивать из неё данные любой таблицы,  с помощью универсального метода обращения к хранимой процедуре и получения данных из таблиц
3. ServiceLayer : IServiceLayer
Слой работы с нашей бд. Позволяет нам извлекать из бд и формировать новый класс Human, который включает в себя 6 классов
4. Converter (IСonverter)
Универсальный парсер, парсит XML и Json. T DeserializeJson(string), T DeserializeXML(string) - методы класса Converter для преобразования JSON или XML.
5. Logger записывает логи в новую базу данных dbo.logs
6. XMLgenerator берет список людей и преобразует в xml файл при этом кидая его в папку SourceDirectory
