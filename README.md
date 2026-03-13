# Overall Data Flow Summary

1. **Client** sends JSON -> **Controller**.
2. **Controller** validates DTO and calls -> **Manager**.
3. **Manager** applies business logic and calls -> **Repository**.
4. **Repository** queries DB and returns Entity -> **Manager**.
5. **Manager** maps Entity to DTO and returns -> **Controller**.
6. **Controller** returns HTTP Result -> **Client**.