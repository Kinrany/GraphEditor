#include <stdio.h>
#include <iostream>
#include <fstream>
#include <string>

extern "C"
{
	__declspec(dllexport) const char* solve(char* file)
	{
		std::string name = file;
		std::string outname = name.substr(0, name.size() - 4) + "c.txt";
		std::ifstream infile(name);
		std::ofstream outfile(outname);
		std::string line;
		std::string delimiter = " ";

		if (infile.is_open())
		{
			while (getline(infile, line))
			{
				outfile << line + "\n";
			}
		}

		infile.close();
		outfile.close();

		const char* cstr = outname.c_str();
		return cstr;
	}
}
