#!/bin/bash
echo $(pwd)
dotnet /app/publish/docker-demo.dll

int=1
while(( $int<=100 ))
do
    echo $int
    sleep 10s
    let "int++"
done
