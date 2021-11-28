
		string name;
		MethodInfo[] methods;
		bool do_timings = false;
		bool verbose = false;
		bool quiet = false;
		int tms = 0;
		DateTime start, end = DateTime.Now;

		iterations = 1;

		var exclude = new Dictionary<string, string> ();
		List<string> run_only = new List<string> ();
		List<string> exclude_test = new List<string> ();
		if (args != null && args.Length > 0) {
			for (j = 0; j < args.Length;) {
				if (args [j] == "--time") {
					do_timings = !quiet;
					j ++;
				} else if (args [j] == "--iter") {
					iterations = Int32.Parse (args [j + 1]);
					j += 2;
				} else if ((args [j] == "-v") || (args [j] == "--verbose")) {
					verbose = !quiet;
					j += 1;
				} else if ((args [j] == "-q") || (args [j] == "--quiet")) {