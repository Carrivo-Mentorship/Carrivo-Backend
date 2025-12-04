#!/usr/bin/env python3
import os
import sys
import argparse

SKIP_USINGS = {
    "using System;",
    "using System.Collections.Generic;",
    "using System.ComponentModel.DataAnnotations.Schema;",
    "using System.Linq;",
    "using System.Text;",
    "using System.Threading.Tasks;",
    "using Shuryan.Core.Entities.Identity;",
    "using Shuryan.Core.Entities.System;",
    "using System.Xml.Linq;",
    "using Shuryan.Core.Entities.Common;",
    "using Shuryan.Core.Entities.Medical;",
    "using Microsoft.AspNetCore.Identity;",
    "using Shuryan.Core.Enums;",
    "using System.ComponentModel;",
    "using System.Reflection;"
}

def should_exclude(path, exclude_dirs):
    parts = [p.lower() for p in path.split(os.sep)]
    return any(ex in parts for ex in exclude_dirs)

def main():
    parser = argparse.ArgumentParser(description="Export all .cs files to a markdown file (skip specific usings).")
    parser.add_argument("project_path", help="Path to project root")
    parser.add_argument("-o", "--output", default="all_sources.md", help="Output markdown file")
    parser.add_argument("--exclude", nargs="*", default=["bin", "obj", ".git", "migrations"], help="Directories to exclude")
    args = parser.parse_args()

    project = os.path.abspath(args.project_path)
    output = os.path.abspath(args.output)

    if not os.path.isdir(project):
        print("Project path not found:", project)
        sys.exit(1)

    if os.path.exists(output):
        os.remove(output)

    entries = []
    for root, dirs, files in os.walk(project):
        dirs[:] = [d for d in dirs if d.lower() not in [e.lower() for e in args.exclude]]
        for file in files:
            if file.lower().endswith(".cs"):
                full_path = os.path.join(root, file)
                rel_path = os.path.relpath(full_path, project)
                entries.append((rel_path, full_path))

    entries.sort()

    with open(output, "w", encoding="utf-8") as wf:
        for rel, full in entries:
            wf.write(f"# {os.path.basename(rel)}\n")
            wf.write("```cs\n")
            with open(full, "r", encoding="utf-8", errors="replace") as rf:
                for line in rf:
                    if line.strip() in SKIP_USINGS:
                        continue
                    wf.write(line)
            wf.write("```\n\n")

    print("Export completed ->", output)

if __name__ == "__main__":
    main()
