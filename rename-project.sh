#!/usr/bin/env bash
# =============================================================================
#  rename-project.sh
#
#  Renames every occurrence of "TechSolve" (and "techsolve") throughout the
#  entire solution — folder names, file names, and file contents — to whatever
#  name you provide.
#
#  Usage:
#    chmod +x rename-project.sh
#    ./rename-project.sh "AcmeCorp"
#
#  Example:
#    ./rename-project.sh "NovaSoft"
#    → TechSolve.API        becomes  NovaSoft.API
#    → TechSolve.sln        becomes  NovaSoft.sln
#    → namespace TechSolve  becomes  namespace NovaSoft
#    → "TechSolve Consulting" in appsettings stays as-is (update manually)
#
#  Safe to run multiple times — it checks if the name is already updated.
# =============================================================================

set -euo pipefail

NEW_NAME="${1:-}"

if [[ -z "$NEW_NAME" ]]; then
  echo "❌  Usage: ./rename-project.sh <NewProjectName>"
  echo "    Example: ./rename-project.sh AcmeCorp"
  exit 1
fi

OLD_NAME="TechSolve"
OLD_LOWER="techsolve"
NEW_LOWER=$(echo "$NEW_NAME" | tr '[:upper:]' '[:lower:]')
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo ""
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo "  Renaming  $OLD_NAME  →  $NEW_NAME"
echo "  Folder:   $SCRIPT_DIR"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo ""

if [[ "$OLD_NAME" == "$NEW_NAME" ]]; then
  echo "⚠️  Nothing to do — name is already '$NEW_NAME'."
  exit 0
fi

# ── Step 1: Replace inside file contents ──────────────────────────────────────
echo "📝  Updating file contents..."

# File extensions to process
EXTENSIONS=("*.cs" "*.csproj" "*.sln" "*.json" "*.ts" "*.html" "*.scss" "*.md" "*.txt" "*.sh")

for EXT in "${EXTENSIONS[@]}"; do
  while IFS= read -r -d '' FILE; do
    # Skip node_modules, bin, obj, dist, .angular
    if echo "$FILE" | grep -qE '/(node_modules|bin|obj|dist|\.angular)/'; then
      continue
    fi
    # Replace TechSolve → NewName  and  techsolve → newname
    sed -i \
      -e "s/${OLD_NAME}/${NEW_NAME}/g" \
      -e "s/${OLD_LOWER}/${NEW_LOWER}/g" \
      "$FILE"
  done < <(find "$SCRIPT_DIR" -name "$EXT" -print0 2>/dev/null)
done

echo "   ✅  File contents updated."

# ── Step 2: Rename files ──────────────────────────────────────────────────────
echo "📄  Renaming files..."

# Must process deepest files first (bottom-up) before renaming parent dirs
while IFS= read -r OLD_PATH; do
  DIR=$(dirname "$OLD_PATH")
  BASE=$(basename "$OLD_PATH")
  NEW_BASE="${BASE//$OLD_NAME/$NEW_NAME}"
  NEW_BASE="${NEW_BASE//$OLD_LOWER/$NEW_LOWER}"
  if [[ "$BASE" != "$NEW_BASE" ]]; then
    mv "$OLD_PATH" "$DIR/$NEW_BASE"
    echo "   $BASE  →  $NEW_BASE"
  fi
done < <(find "$SCRIPT_DIR" -not -path "*/node_modules/*" -not -path "*/.git/*" \
         -not -path "*/bin/*" -not -path "*/obj/*" \
         -type f | sort -r)

echo "   ✅  Files renamed."

# ── Step 3: Rename directories ────────────────────────────────────────────────
echo "📁  Renaming directories..."

# Process deepest directories first
while IFS= read -r OLD_DIR; do
  PARENT=$(dirname "$OLD_DIR")
  BASE=$(basename "$OLD_DIR")
  NEW_BASE="${BASE//$OLD_NAME/$NEW_NAME}"
  NEW_BASE="${NEW_BASE//$OLD_LOWER/$NEW_LOWER}"
  if [[ "$BASE" != "$NEW_BASE" ]]; then
    mv "$OLD_DIR" "$PARENT/$NEW_BASE"
    echo "   $BASE/  →  $NEW_BASE/"
  fi
done < <(find "$SCRIPT_DIR" -not -path "*/node_modules/*" -not -path "*/.git/*" \
         -not -path "*/bin/*" -not -path "*/obj/*" \
         -mindepth 1 -type d | sort -r)

echo "   ✅  Directories renamed."

# ── Step 4: Rename the script's own parent folder (optional) ──────────────────
echo ""
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo "  ✅  Rename complete!"
echo ""
echo "  Next steps:"
echo "  1. Update the display name in:"
echo "     • TechSolve.Domain/Constants/AppConstants.cs  (AppName, AdminEmail, etc.)"
echo "     • TechSolve.UI/clientApp/src/index.html       (page title)"
echo "     • appsettings.json                            (EmailSettings.FromName)"
echo ""
echo "  2. Run migrations with the new project name:"
echo "     dotnet ef migrations add InitialCreate \\"
echo "       --project ${NEW_NAME}.DataModel \\"
echo "       --startup-project ${NEW_NAME}.API"
echo ""
echo "  3. Rebuild:"
echo "     dotnet build ${NEW_NAME}.sln"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
