namespace AoC;

public class FileSystem(List<FileSystem.Block> blocks)
{
    public record Block(int Id, int Size)
    {
        public bool IsFreeSpace => Id < 0;

        public long ComputeCheckSum(int position)
        {
            if (IsFreeSpace)
            {
                return 0;
            }

            long checksum = 0;
            for (int i = 0; i < Size; i++)
            {
                checksum += (position + i) * Id;
            }
            return checksum;
        }

        public void Print()
        {
            if (IsFreeSpace)
            {
                Console.Write(new string('.', Size));
            }
            else
            {
                char blockChar = (char)('0' + Id);
                Console.Write(new string(blockChar, Size));
            }
        }
    }

    public List<Block> Blocks => blocks;
    public Block LastFileBlock => blocks.Last(b => !b.IsFreeSpace);

    public static FileSystem Parse(string line)
    {
        List<Block> blocks = [];
        int currentId = 0;
        for (int i = 0; i < line.Length; i += 2)
        {
            int size = line[i] - '0';
            blocks.Add(new(currentId, size));
            if (i + 1 < line.Length)
            {
                size = line[i + 1] - '0';
                blocks.Add(new(-1, size));
            }

            currentId++;
        }
        return new(blocks);
    }

    public void Defragment()
    {
        for (int i = 1; i < blocks.Count; i++)
        {
            Block nextBlock = blocks[i];
            if (nextBlock.IsFreeSpace)
            {
                if (i >= blocks.Count - 2 || nextBlock.Size == 0)
                {
                    blocks.RemoveAt(i);
                }
                else
                {
                    int nrFreeBlocks = nextBlock.Size;
                    if (LastFileBlock.Size > nrFreeBlocks)
                    {
                        blocks.RemoveAt(i);
                        blocks.Insert(i, new(LastFileBlock.Id, nrFreeBlocks));
                        ResizeLastFileBlock(LastFileBlock.Size - nrFreeBlocks);
                    }
                    else
                    {
                        int lastFileBlockSize = LastFileBlock.Size;
                        blocks.RemoveAt(i);
                        MoveLastBlockTo(i);
                        if (i + 1 < blocks.Count)
                        {
                            blocks.Insert(i + 1, new(-1, nrFreeBlocks - lastFileBlockSize));
                        }
                        else
                        {
                            blocks.Add(new(-1, nrFreeBlocks - lastFileBlockSize));
                        }
                        // Loop will continue with next free space block we just inserted.
                    }
                }
            }
        }
    }

    public long ComputeChecksum()
    {
        int position = 0;
        long checksum = 0;
        foreach (Block block in blocks)
        {
            checksum += block.ComputeCheckSum(position);
            position += block.Size;
        }

        return checksum;
    }

    private void MoveLastBlockTo(int newIndex)
    {
        blocks.Insert(newIndex, LastFileBlock);
        RemoveLastFileBlock();
    }

    private void RemoveLastFileBlock()
    {
        int lastFileBlockIndex = blocks.LastIndexOf(LastFileBlock);
        if (lastFileBlockIndex < blocks.Count - 1)
        {
            blocks.RemoveRange(lastFileBlockIndex + 1, blocks.Count - lastFileBlockIndex - 1);
        }
        blocks.RemoveAt(lastFileBlockIndex);

        while (LastFileBlock.IsFreeSpace)
        {
            blocks.RemoveAt(blocks.Count - 1);
        }
    }

    private void ResizeLastFileBlock(int newSize)
    {
        int id = LastFileBlock.Id;
        RemoveLastFileBlock();
        blocks.Add(new(id, newSize));
    }

    public void DefragmentWholeBlocks()
    {
        for (int i = blocks.Count - 1; i > 1; i--)
        {
            Block currentBlock = blocks[i];
            if (!currentBlock.IsFreeSpace)
            {
                int sizeNeeded = currentBlock.Size;
                Block? freeBlock = blocks.FirstOrDefault(b => b.IsFreeSpace && b.Size >= sizeNeeded);
                if (freeBlock != null)
                {
                    int fittingFreeSpaceIndex = blocks.IndexOf(freeBlock);
                    if (fittingFreeSpaceIndex < i)
                    {
                        InsertFreeBlockAt(i + 1, currentBlock.Size);
                        blocks.RemoveAt(fittingFreeSpaceIndex);

                        // We have to use i - 1 because we just removed a block.
                        MoveBlockTo(i - 1, fittingFreeSpaceIndex);

                        if (freeBlock.Size > sizeNeeded)
                        {
                            InsertFreeBlockAt(fittingFreeSpaceIndex + 1, freeBlock.Size - sizeNeeded);
                            // We added an extra block, so we need to correct i.
                            i++;
                        }
                    }
                }
            }
        }

        CombineFreeSpace();
    }

    private void CombineFreeSpace()
    {
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            if (blocks[i].IsFreeSpace && blocks[i + 1].IsFreeSpace)
            {
                int combinedSize = blocks[i].Size + blocks[i + 1].Size;
                blocks.RemoveRange(i, 2);
                InsertFreeBlockAt(i, combinedSize);

                // Retry to combine with the current block again.
                i--;
            }
        }
    }

    public void Print()
    {
        foreach (Block block in blocks)
        {
            block.Print();
        }
        Console.WriteLine();
    }

    private void MoveBlockTo(int srcBlockIndex, int destBlockIndex)
    {
        Block block = blocks[srcBlockIndex];
        blocks.RemoveAt(srcBlockIndex);
        blocks.Insert(destBlockIndex, block);
    }

    private void InsertFreeBlockAt(int blockIndex, int size)
    {
        blocks.Insert(blockIndex, new Block(-1, size));
    }

}
