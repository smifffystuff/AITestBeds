




class Demographics
{
    [KernelFunction]
    public int GetPersonAge(string name)
    {
        return name switch
        {
            "Martin" => 62,
            _ => 20
        };
    }
}


