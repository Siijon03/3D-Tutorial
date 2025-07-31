using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue Asset")]
public class DialogueAsset : ScriptableObject
{
    [TextArea]
    public string[] introDialogue;

    public Question question;

    [System.Serializable]
    public class Question
    {
        public string questionText;

        public Response[] responses;
    }

    [System.Serializable]
    public class Response
    {
        public string playerChoice;
        [TextArea]
        public string npcResponse;
    }
}
