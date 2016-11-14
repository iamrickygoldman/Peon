[<EntryPoint>]
let main argv = 
    let path = "e:\\NET\TEST"
    let dir = new System.IO.DirectoryInfo(path)
    let extensions = PeonHelper.getAudioMimeTypes
    for artist in dir.GetDirectories() do
        printfn "Artist: %s" artist.Name
        for album in artist.GetDirectories() do
            printfn "\t Album: %s" album.Name
            for song in album.GetFiles() do
                if (Array.contains song.Extension extensions) then
                    let mutable songTags = null;
                    try
                        songTags <- TagLib.File.Create(song.FullName).Tag
                    with
                        | :? TagLib.UnsupportedFormatException -> printfn "File format not allowed!"
                    
                    let mutable trackString = songTags.Track.ToString() + " - ";

                    if (songTags.Track = 0u) then
                        trackString <- "";
                    elif (songTags.Track < 10u) then
                        trackString <- "0" + songTags.Track.ToString() + " - "
                    let newName:string = song.DirectoryName + "\\" + trackString + PeonHelper.cleanFileName songTags.Title + song.Extension
                    song.MoveTo(newName)
                    printfn "\t\t Song: %s" (song.FullName)

    System.Console.ReadKey() |> ignore
    0