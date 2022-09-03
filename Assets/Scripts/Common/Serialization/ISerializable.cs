namespace Common.Serialization
{
    /// <summary>
    /// интерфейс сериализуемого объекта
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// сериализует объект
        /// </summary>
        /// <returns>сериализованная строка</returns>
        string GetSerializationString();

        /// <summary>
        /// десериализует строку в объект
        /// </summary>
        /// <param name="serializationString">сериализованная строка</param>
        void SetSerializationString(string serializationString);
    }
}