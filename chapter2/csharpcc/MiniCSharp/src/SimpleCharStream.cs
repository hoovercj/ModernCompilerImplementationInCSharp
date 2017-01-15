/* Generated By:CSharpCC: Do not edit this line. SimpleCharStream.cs Version 1.0.0.0 */
/* CSharpCCOptions:STATIC=False,SUPPORT_CLASS_VISIBILITY_PUBLIC=True */
namespace ModernCompilerImplementation.Chapter2.CSharpCC.Lib{


using System;

 /// <summary>
 /// An implementation of interface <see cref="ICharStream"/>, where the 
 /// stream is assumed to contain only ASCII characters (without unicode 
 /// processing).
 /// </summary>
public class SimpleCharStream
{
/** Whether parser is static. */
  public const bool staticFlag = false;
  int bufsize;
  int available;
  int tokenBegin;
/** Position in buffer. */
  public int bufpos = -1;
  protected int[] bufline;
  protected int[] bufcolumn;

  protected int column = 0;
  protected int line = 1;

  protected bool prevCharIsCR = false;
  protected bool prevCharIsLF = false;

  protected System.IO.TextReader inputStream;

  protected char[] buffer;
  protected int maxNextCharInd = 0;
  protected int inBuf = 0;
  protected int tabSize = 8;

  protected int TabSize { get; set; }


  protected void ExpandBuff(bool wrapAround)
  {
    char[] newbuffer = new char[bufsize + 2048];
    int[] newbufline = new int[bufsize + 2048];
    int[] newbufcolumn = new int[bufsize + 2048];

    try
    {
      if (wrapAround)
      {
        Array.Copy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);
        Array.Copy(buffer, 0, newbuffer, bufsize - tokenBegin, bufpos);
        buffer = newbuffer;

        Array.Copy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);
        Array.Copy(bufline, 0, newbufline, bufsize - tokenBegin, bufpos);
        bufline = newbufline;

        Array.Copy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);
        Array.Copy(bufcolumn, 0, newbufcolumn, bufsize - tokenBegin, bufpos);
        bufcolumn = newbufcolumn;

        maxNextCharInd = (bufpos += (bufsize - tokenBegin));
      }
      else
      {
        Array.Copy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);
        buffer = newbuffer;

        Array.Copy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);
        bufline = newbufline;

        Array.Copy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);
        bufcolumn = newbufcolumn;

        maxNextCharInd = (bufpos -= tokenBegin);
      }
    }
    catch (Exception e)
    {
      throw new InvalidOperationException(e.Message);
    }


    bufsize += 2048;
    available = bufsize;
    tokenBegin = 0;
  }

  protected void FillBuff()
  {
    if (maxNextCharInd == available)
    {
      if (available == bufsize)
      {
        if (tokenBegin > 2048)
        {
          bufpos = maxNextCharInd = 0;
          available = tokenBegin;
        }
        else if (tokenBegin < 0)
          bufpos = maxNextCharInd = 0;
        else
          ExpandBuff(false);
      }
      else if (available > tokenBegin)
        available = bufsize;
      else if ((tokenBegin - available) < 2048)
        ExpandBuff(true);
      else
        available = tokenBegin;
    }

    int i;
    try {
      if ((i = inputStream.Read(buffer, maxNextCharInd, available - maxNextCharInd)) == -1)
      {
        inputStream.Dispose();
        throw new System.IO.IOException();
      }
      else
        maxNextCharInd += i;
      return;
    }
    catch(System.IO.IOException e) {
      --bufpos;
      Backup(0);
      if (tokenBegin == -1)
        tokenBegin = bufpos;
      throw e;
    }
  }

/** Start. */
  public char BeginToken()
  {
    try {
    tokenBegin = -1;
    char c = ReadChar();
    tokenBegin = bufpos;

    return c;
	} catch (System.IO.EndOfStreamException) {
		if (tokenBegin == -1)
			tokenBegin = bufpos;
		throw;
	}
  }

  protected void UpdateLineColumn(char c)
  {
    column++;

    if (prevCharIsLF)
    {
      prevCharIsLF = false;
      line += (column = 1);
    }
    else if (prevCharIsCR)
    {
      prevCharIsCR = false;
      if (c == '\n')
      {
        prevCharIsLF = true;
      }
      else
        line += (column = 1);
    }

    switch (c)
    {
      case '\r' :
        prevCharIsCR = true;
        break;
      case '\n' :
        prevCharIsLF = true;
        break;
      case '\t' :
        column--;
        column += (tabSize - (column % tabSize));
        break;
      default :
        break;
    }

    bufline[bufpos] = line;
    bufcolumn[bufpos] = column;
  }

/** Read a character. */
  public char ReadChar()
  {
    if (inBuf > 0)
    {
      --inBuf;

      if (++bufpos == bufsize)
        bufpos = 0;

      return buffer[bufpos];
    }

    if (++bufpos >= maxNextCharInd)
      FillBuff();

	  if (bufpos >= maxNextCharInd) {
		bufpos--;
		if (bufpos < 0)
			bufpos += bufsize;
		throw new System.IO.EndOfStreamException();
	  }

    char c = buffer[bufpos];

    UpdateLineColumn(c);
    return c;
  }

  public int Column {
	get {
    return bufcolumn[bufpos];
	}
  }

  public int Line {
	get {
    return bufline[bufpos];
	}
  }

  /** Get token end column number. */
  public int EndColumn {
	get {
    return bufcolumn[bufpos];
	}
  }

  /** Get token end line number. */
  public int EndLine {
	get {
     return bufline[bufpos];
	}
  }

  /** Get token beginning column number. */
  public int BeginColumn {
	get {
    return bufcolumn[tokenBegin];
	}
  }

  /** Get token beginning line number. */
  public int BeginLine {
	get {
    return bufline[tokenBegin];
	}
  }

/** Backup a number of characters. */
  public void Backup(int amount) {

    inBuf += amount;
    if ((bufpos -= amount) < 0)
      bufpos += bufsize;
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.TextReader dstream, int startline, int startcolumn, int buffersize)
  {
    inputStream = dstream;
    line = startline;
    column = startcolumn - 1;

    available = bufsize = buffersize;
    buffer = new char[buffersize];
    bufline = new int[buffersize];
    bufcolumn = new int[buffersize];
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.TextReader dstream, int startline, int startcolumn)
    : this(dstream, startline, startcolumn, 4096) {
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.TextReader dstream)
    : this(dstream, 1, 1, 4096) {
  }

  /** Reinitialise. */
  public void ReInit(System.IO.TextReader dstream, int startline, int startcolumn, int buffersize)
  {
    inputStream = dstream;
    line = startline;
    column = startcolumn - 1;

    if (buffer == null || buffersize != buffer.Length)
    {
      available = bufsize = buffersize;
      buffer = new char[buffersize];
      bufline = new int[buffersize];
      bufcolumn = new int[buffersize];
    }
    prevCharIsLF = prevCharIsCR = false;
    tokenBegin = inBuf = maxNextCharInd = 0;
    bufpos = -1;
  }

  /** Reinitialise. */
  public void ReInit(System.IO.TextReader dstream, int startline, int startcolumn)
  {
    ReInit(dstream, startline, startcolumn, 4096);
  }

  /** Reinitialise. */
  public void ReInit(System.IO.TextReader dstream)
  {
    ReInit(dstream, 1, 1, 4096);
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.Stream dstream, System.Text.Encoding encoding, int startline, int startcolumn, int buffersize)
    : this(encoding == null ? new System.IO.StreamReader(dstream) : new System.IO.StreamReader(dstream, encoding), startline, startcolumn, buffersize) {
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.Stream dstream, int startline, int startcolumn, int buffersize)
    : this(new System.IO.StreamReader(dstream), startline, startcolumn, buffersize) {
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.Stream dstream, System.Text.Encoding encoding, int startline, int startcolumn)
    : this(dstream, encoding, startline, startcolumn, 4096) {
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.Stream dstream, int startline, int startcolumn)
    : this(dstream, startline, startcolumn, 4096) {
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.Stream dstream, System.Text.Encoding encoding)
    : this(dstream, encoding, 1, 1, 4096) {
  }

  /** Constructor. */
  public SimpleCharStream(System.IO.Stream dstream)
    : this(dstream, 1, 1, 4096) {
  }

  /** Reinitialise. */
  public void ReInit(System.IO.Stream dstream, System.Text.Encoding encoding, int startline, int startcolumn, int buffersize)
  {
    ReInit(encoding == null ? new System.IO.StreamReader(dstream) : new System.IO.StreamReader(dstream, encoding), startline, startcolumn, buffersize);
  }

  /** Reinitialise. */
  public void ReInit(System.IO.Stream dstream, int startline, int startcolumn, int buffersize)
  {
    ReInit(new System.IO.StreamReader(dstream), startline, startcolumn, buffersize);
  }

  /** Reinitialise. */
  public void ReInit(System.IO.Stream dstream, System.Text.Encoding encoding)
  {
    ReInit(dstream, encoding, 1, 1, 4096);
  }

  /** Reinitialise. */
  public void ReInit(System.IO.Stream dstream)
  {
    ReInit(dstream, 1, 1, 4096);
  }
  /** Reinitialise. */
  public void ReInit(System.IO.Stream dstream, System.Text.Encoding encoding, int startline, int startcolumn)
  {
    ReInit(dstream, encoding, startline, startcolumn, 4096);
  }
  /** Reinitialise. */
  public void ReInit(System.IO.Stream dstream, int startline, int startcolumn)
  {
    ReInit(dstream, startline, startcolumn, 4096);
  }
  /** Get token literal value. */
  public String GetImage()
  {
    if (bufpos >= tokenBegin)
      return new String(buffer, tokenBegin, bufpos - tokenBegin + 1);
    else
      return new String(buffer, tokenBegin, bufsize - tokenBegin) + new String(buffer, 0, bufpos + 1);
  }

  /** Get the suffix. */
  public char[] GetSuffix(int len)
  {
    char[] ret = new char[len];

    if ((bufpos + 1) >= len)
      Array.Copy(buffer, bufpos - len + 1, ret, 0, len);
    else
    {
      Array.Copy(buffer, bufsize - (len - bufpos - 1), ret, 0, len - bufpos - 1);
      Array.Copy(buffer, 0, ret, len - bufpos - 1, bufpos + 1);
    }

    return ret;
  }

  /** Reset buffer when finished. */
  public void Done()
  {
    buffer = null;
    bufline = null;
    bufcolumn = null;
  }

  /**
   * Method to adjust line and column numbers for the start of a token.
   */
  public void AdjustBeginLineColumn(int newLine, int newCol)
  {
    int start = tokenBegin;
    int len;

    if (bufpos >= tokenBegin)
    {
      len = bufpos - tokenBegin + inBuf + 1;
    }
    else
    {
      len = bufsize - tokenBegin + bufpos + 1 + inBuf;
    }

    int i = 0, j = 0, k = 0;
    int nextColDiff = 0, columnDiff = 0;

    while (i < len && bufline[j = start % bufsize] == bufline[k = ++start % bufsize])
    {
      bufline[j] = newLine;
      nextColDiff = columnDiff + bufcolumn[k] - bufcolumn[j];
      bufcolumn[j] = newCol + columnDiff;
      columnDiff = nextColDiff;
      i++;
    }

    if (i < len)
    {
      bufline[j] = newLine++;
      bufcolumn[j] = newCol + columnDiff;

      while (i++ < len)
      {
        if (bufline[j = start % bufsize] != bufline[++start % bufsize])
          bufline[j] = newLine++;
        else
          bufline[j] = newLine;
      }
    }

    line = bufline[j];
    column = bufcolumn[j];
  }

}
}
/* CSharpCC - OriginalChecksum=d41d8cd98f00b204e9800998ecf8427e (do not edit this line) */
