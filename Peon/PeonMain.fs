[<EntryPoint>]
let main argv = 
    if argv.Length = 0 then
        printfn "You must specify either a full directory path or use %sCD%s for current directory" "%" "%"
    elif argv.[0] = "--help" then
        printfn "Peon.exe music_dir [format] [space_replace]";
        printfn "\n\r music_dir -> absolute path to top level music directory"
        printfn "    format -> music file naming format e.g. ?N - ?T"
        printfn "underscore -> replace spaces with entered value"
        printfn "\n\rFor full list of format values, use --help-format."
        printfn "\n\rDirectories must be structured as:"
        printfn "Top"
        printfn "|-Artist_Name"
        printfn "|-|-Album_Name"
    elif (argv.[0] = "--help-format") then
        printfn "Format Values:"
        printfn "?N -> track number with leading 0 (ex. 09)"
        printfn "?n -> track number (ex: 9)"
        printfn "?T -> title"
        printfn "?t -> title in lowercase"
        printfn "?A -> artist"
        printfn "?a -> artist in lowercase"
        printfn "?B -> album"
        printfn "?b -> album in lowercase"
    else
        let path = argv.[0]
        let mutable format = "?N - ?T"
        if argv.Length > 1 then
            format <- argv.[1]
        if not (format.Contains("?")) then
            printfn "Your format string doesn't contain any wildcards and will not generate unique filenames."
        else
            try
                let dir = new System.IO.DirectoryInfo(path)
                let extensions = PeonHelper.getAudioMimeTypes
                for artist in dir.GetDirectories() do
                    printfn "Artist: %s" artist.Name
                    for album in artist.GetDirectories() do
                        printfn "\t Album: %s" album.Name
                        for song in album.GetFiles() do
                            if (Array.contains song.Extension extensions) then
                                let mutable songTags = null
                                try
                                    songTags <- TagLib.File.Create(song.FullName)
                                with
                                    | :? TagLib.UnsupportedFormatException -> printfn "File format not allowed!"
                                let mutable newName:string = PeonHelper.formatName format songTags
                                if argv.Length > 2 then
                                    newName <- newName.Replace(" ",argv.[2])
                                song.MoveTo(song.DirectoryName + "\\" + newName + song.Extension)
                                printfn "\t\t Song: %s" (song.FullName)
            with
                | :? System.ArgumentNullException -> printfn "Path is null."
                | :? System.Security.SecurityException -> printfn "You lack permission to edit this directory."
                | :? System.ArgumentException -> printfn "Path contains invalid characters."
                | :? System.IO.PathTooLongException -> printfn "Path name is too long."
                | :? System.IO.DirectoryNotFoundException -> printfn "Directory not found."
                | :? System.IO.IOException -> printfn "Path is a file. Choose a directory."

    printfn "\n\rPress any key to exit"
    System.Console.ReadKey() |> ignore
    0