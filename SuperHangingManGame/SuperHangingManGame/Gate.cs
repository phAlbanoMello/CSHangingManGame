using SuperHangingManGame;

class Gate
{
    public string Theme { get; }
    public string CorrectWord { get; }
    public string CurrentProgress { get; private set; }
    public bool IsUnlocked { get; private set; }
    public Lock[] Locks { get; internal set; }

    public Gate(string theme, string correctWord, int level)
    {
        Theme = theme;
        CorrectWord = correctWord.ToUpper();
        CurrentProgress = new string('_', CorrectWord.Length);

    }

    public int GetLockCount()
    { 
        return (int)Locks.GetHashCode();
    }

    public bool GuessLetter(char letter)
    {
        letter = char.ToUpper(letter);

        if (CorrectWord.Contains(letter))
        {
            // Update current progress with correctly guessed letter
            char[] progressArray = CurrentProgress.ToCharArray();
            for (int i = 0; i < CorrectWord.Length; i++)
            {
                if (CorrectWord[i] == letter)
                {
                    progressArray[i] = letter;
                }
            }

            CurrentProgress = new string(progressArray);

            // Check if the word is fully guessed
            if (CurrentProgress == CorrectWord)
            {
                IsUnlocked = true;
            }

            return true;
        }

        return false;
    }
}
